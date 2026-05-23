using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;

public class WithdrawalOrderService : IWithdrawalOrderService
{
    private const int DefaultCustomerId = 1;

    private readonly IUnitOfWork _unitOfWork;
    private readonly WMSDbContext _dbContext;

    public WithdrawalOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dbContext = new WMSDbContext();
    }

    public async Task<List<OrderSummaryDto>> GetOrderSummariesAsync()
    {
        var orders = await _dbContext.WithdrawalOrders
            .Where(o => o.WarehouseId == WarehouseConstants.DefaultWarehouseId)
            .Include(o => o.WithdrawalOrderDetails)
            .OrderByDescending(o => o.OrderDate)
            .ThenByDescending(o => o.CreatedAt)
            .ToListAsync();

        return orders.Select(o => new OrderSummaryDto
        {
            OrderId = o.Id,
            OrderNumber = o.OrderNumber,
            OrderDate = o.OrderDate,
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt,
            OrderType = "XUẤT",
            LineCount = o.WithdrawalOrderDetails.Count,
            TotalQuantity = o.WithdrawalOrderDetails.Sum(d => d.Quantity)
        }).ToList();
    }

    public async Task<OrderInvoiceDto?> GetInvoiceAsync(int orderId)
    {
        var order = await _dbContext.WithdrawalOrders
            .Include(o => o.Warehouse)
            .Include(o => o.Customer)
            .Include(o => o.WithdrawalOrderDetails)
                .ThenInclude(d => d.Item)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return null;

        return new OrderInvoiceDto
        {
            Title = "HÓA ĐƠN XUẤT KHO",
            OrderType = "XUẤT",
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            WarehouseName = order.Warehouse?.Name ?? "",
            PartnerLabel = "Người nhận",
            PartnerName = string.IsNullOrWhiteSpace(order.RecipientName)
                ? order.Customer?.Name ?? ""
                : order.RecipientName,
            Lines = order.WithdrawalOrderDetails.Select((d, i) => new OrderInvoiceLineDto
            {
                ItemId = d.ItemId,
                LineNumber = i + 1,
                ItemCode = d.Item?.Code ?? "",
                ItemName = d.Item?.Name ?? "",
                MeasurementUnit = d.Item?.MeasurementUnit ?? "",
                Quantity = d.Quantity
            }).ToList()
        };
    }

    public async Task<int> CreateOrderAsync(DateTime orderDate, string recipientName, List<WithdrawalOrderLineInputDto> lines)
    {
        if (lines == null || lines.Count == 0)
            throw new ArgumentException("Phiếu xuất phải có ít nhất một dòng hàng.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = new WithdrawalOrder
            {
                WarehouseId = WarehouseConstants.DefaultWarehouseId,
                CustomerId = DefaultCustomerId,
                OrderNumber = GenerateOrderNumber("PX"),
                OrderDate = orderDate,
                RecipientName = recipientName?.Trim() ?? "",
                CreatedAt = DateTime.Now,
                WithdrawalOrderDetails = new List<WithdrawalOrderDetail>()
            };

            foreach (var line in lines)
            {
                await ApplyStockDecreaseAsync(line.ItemId, line.Quantity);
                order.WithdrawalOrderDetails.Add(new WithdrawalOrderDetail
                {
                    ItemId = line.ItemId,
                    Quantity = line.Quantity,
                    CreatedAt = DateTime.Now
                });
            }

            _dbContext.WithdrawalOrders.Add(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            await ActivityLogger.LogAsync(_dbContext, "Xuất", "Phiếu xuất",
                $"Tạo phiếu xuất {order.OrderNumber}, người nhận: {order.RecipientName}, tổng SL {lines.Sum(l => l.Quantity)}",
                order.OrderNumber);
            return order.Id;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateOrderAsync(int orderId, DateTime orderDate, string recipientName, List<WithdrawalOrderLineInputDto> lines)
    {
        if (lines == null || lines.Count == 0)
            throw new ArgumentException("Phiếu xuất phải có ít nhất một dòng hàng.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = await _dbContext.WithdrawalOrders
                .Include(o => o.WithdrawalOrderDetails)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new InvalidOperationException("Không tìm thấy phiếu xuất.");

            foreach (var detail in order.WithdrawalOrderDetails.ToList())
                await ApplyStockIncreaseAsync(detail.ItemId, detail.Quantity);

            _dbContext.Set<WithdrawalOrderDetail>().RemoveRange(order.WithdrawalOrderDetails);
            order.WithdrawalOrderDetails.Clear();
            order.OrderDate = orderDate;
            order.RecipientName = recipientName?.Trim() ?? "";
            order.UpdatedAt = DateTime.Now;

            foreach (var line in lines)
            {
                await ApplyStockDecreaseAsync(line.ItemId, line.Quantity);
                order.WithdrawalOrderDetails.Add(new WithdrawalOrderDetail
                {
                    ItemId = line.ItemId,
                    Quantity = line.Quantity,
                    CreatedAt = DateTime.Now
                });
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            await ActivityLogger.LogAsync(_dbContext, "Sửa", "Phiếu xuất",
                $"Cập nhật phiếu xuất {order.OrderNumber}", order.OrderNumber);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = await _dbContext.WithdrawalOrders
                .Include(o => o.WithdrawalOrderDetails)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new InvalidOperationException("Không tìm thấy phiếu xuất.");

            foreach (var detail in order.WithdrawalOrderDetails)
                await ApplyStockIncreaseAsync(detail.ItemId, detail.Quantity);

            _dbContext.Set<WithdrawalOrderDetail>().RemoveRange(order.WithdrawalOrderDetails);
            var orderNumber = order.OrderNumber;
            _dbContext.WithdrawalOrders.Remove(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            await ActivityLogger.LogAsync(_dbContext, "Xóa", "Phiếu xuất",
                $"Xóa phiếu xuất {orderNumber}", orderNumber);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task ApplyStockDecreaseAsync(int itemId, int quantity)
    {
        var stock = await _dbContext.StockItems
            .FirstOrDefaultAsync(s => s.WarehouseId == WarehouseConstants.DefaultWarehouseId && s.ItemId == itemId);

        if (stock == null || stock.Quantity < quantity)
        {
            var item = await _dbContext.Items.FindAsync(itemId);
            throw new InvalidOperationException($"Tồn kho không đủ cho hàng: {item?.Name ?? itemId.ToString()}");
        }

        stock.Quantity -= quantity;
        stock.UpdatedAt = DateTime.Now;
    }

    private async Task ApplyStockIncreaseAsync(int itemId, int quantity)
    {
        var stock = await _dbContext.StockItems
            .FirstOrDefaultAsync(s => s.WarehouseId == WarehouseConstants.DefaultWarehouseId && s.ItemId == itemId)
            ?? throw new InvalidOperationException("Không tìm thấy tồn kho để hoàn tác.");

        stock.Quantity += quantity;
        stock.UpdatedAt = DateTime.Now;
    }

    private static string GenerateOrderNumber(string prefix) =>
        $"{prefix}-{DateTime.Now:yyyyMMdd-HHmmss}";
}
