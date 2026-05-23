using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;

public class SupplyOrderService : ISupplyOrderService
{
    private const int PreferredDefaultSupplierId = 1;

    private readonly IUnitOfWork _unitOfWork;
    private readonly WMSDbContext _dbContext;

    public SupplyOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dbContext = new WMSDbContext();
    }

    public async Task<List<OrderSummaryDto>> GetOrderSummariesAsync()
    {
        var orders = await _dbContext.SupplyOrders
            .Where(o => o.WarehouseId == WarehouseConstants.DefaultWarehouseId)
            .Include(o => o.SupplyOrderDetails)
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
            OrderType = "NHẬP",
            LineCount = o.SupplyOrderDetails.Count,
            TotalQuantity = o.SupplyOrderDetails.Sum(d => d.Quantity)
        }).ToList();
    }

    public async Task<OrderInvoiceDto?> GetInvoiceAsync(int orderId)
    {
        var order = await _dbContext.SupplyOrders
            .Include(o => o.Warehouse)
            .Include(o => o.Supplier)
            .Include(o => o.SupplyOrderDetails)
                .ThenInclude(d => d.Item)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return null;

        return new OrderInvoiceDto
        {
            Title = "HÓA ĐƠN NHẬP KHO",
            OrderType = "NHẬP",
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            WarehouseName = order.Warehouse?.Name ?? "",
            PartnerLabel = "Nhà cung cấp",
            PartnerName = order.Supplier?.Name ?? "N/A",
            Lines = order.SupplyOrderDetails.Select((d, i) => new OrderInvoiceLineDto
            {
                ItemId = d.ItemId,
                LineNumber = i + 1,
                ItemCode = d.Item?.Code ?? "",
                ItemName = d.Item?.Name ?? "",
                MeasurementUnit = d.Item?.MeasurementUnit ?? "",
                Quantity = d.Quantity,
                ProductionDate = d.ProductionDate,
                ExpirationDate = d.ExpirationDate
            }).ToList()
        };
    }

    public async Task<int> CreateOrderAsync(DateTime orderDate, List<SupplyOrderLineInputDto> lines)
    {
        if (lines == null || lines.Count == 0)
            throw new ArgumentException("Phiếu nhập phải có ít nhất một dòng hàng.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = new SupplyOrder
            {
                WarehouseId = WarehouseConstants.DefaultWarehouseId,
                SupplierId = await GetDefaultSupplierIdAsync(),
                OrderNumber = GenerateOrderNumber("PN"),
                OrderDate = orderDate,
                CreatedAt = DateTime.Now,
                SupplyOrderDetails = new List<SupplyOrderDetail>()
            };

            foreach (var line in lines)
            {
                var item = await ResolveItemAsync(line);
                order.SupplyOrderDetails.Add(new SupplyOrderDetail
                {
                    ItemId = item.Id,
                    Quantity = line.Quantity,
                    ProductionDate = line.ProductionDate,
                    ExpirationDate = line.ExpirationDate,
                    CreatedAt = DateTime.Now
                });
                await ApplyStockIncreaseAsync(item.Id, line.Quantity, line.ProductionDate, line.ExpirationDate);
            }

            _dbContext.SupplyOrders.Add(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            await ActivityLogger.LogAsync(_dbContext, "Nhập", "Phiếu nhập",
                $"Tạo phiếu nhập {order.OrderNumber}, {lines.Count} dòng, tổng SL {lines.Sum(l => l.Quantity)}",
                order.OrderNumber);
            return order.Id;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateOrderAsync(int orderId, DateTime orderDate, List<SupplyOrderLineInputDto> lines)
    {
        if (lines == null || lines.Count == 0)
            throw new ArgumentException("Phiếu nhập phải có ít nhất một dòng hàng.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = await _dbContext.SupplyOrders
                .Include(o => o.SupplyOrderDetails)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new InvalidOperationException("Không tìm thấy phiếu nhập.");

            foreach (var detail in order.SupplyOrderDetails.ToList())
                await ApplyStockDecreaseAsync(detail.ItemId, detail.Quantity);

            _dbContext.Set<SupplyOrderDetail>().RemoveRange(order.SupplyOrderDetails);
            order.SupplyOrderDetails.Clear();
            order.OrderDate = orderDate;
            order.UpdatedAt = DateTime.Now;

            foreach (var line in lines)
            {
                var item = await ResolveItemAsync(line);
                order.SupplyOrderDetails.Add(new SupplyOrderDetail
                {
                    ItemId = item.Id,
                    Quantity = line.Quantity,
                    ProductionDate = line.ProductionDate,
                    ExpirationDate = line.ExpirationDate,
                    CreatedAt = DateTime.Now
                });
                await ApplyStockIncreaseAsync(item.Id, line.Quantity, line.ProductionDate, line.ExpirationDate);
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            await ActivityLogger.LogAsync(_dbContext, "Sửa", "Phiếu nhập",
                $"Cập nhật phiếu nhập {order.OrderNumber}", order.OrderNumber);
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
            var order = await _dbContext.SupplyOrders
                .Include(o => o.SupplyOrderDetails)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new InvalidOperationException("Không tìm thấy phiếu nhập.");

            foreach (var detail in order.SupplyOrderDetails)
                await ApplyStockDecreaseAsync(detail.ItemId, detail.Quantity);

            _dbContext.Set<SupplyOrderDetail>().RemoveRange(order.SupplyOrderDetails);
            var orderNumber = order.OrderNumber;
            _dbContext.SupplyOrders.Remove(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            await ActivityLogger.LogAsync(_dbContext, "Xóa", "Phiếu nhập",
                $"Xóa phiếu nhập {orderNumber}", orderNumber);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<Item> ResolveItemAsync(SupplyOrderLineInputDto line)
    {
        if (line.ItemId.HasValue && line.ItemId.Value > 0)
        {
            var existing = await _dbContext.Items.FindAsync(line.ItemId.Value);
            if (existing != null) return existing;
        }

        var byCode = await _dbContext.Items.FirstOrDefaultAsync(i => i.Code == line.ItemCode);
        if (byCode != null)
        {
            byCode.Name = line.ItemName;
            byCode.MeasurementUnit = line.MeasurementUnit;
            byCode.Category = line.Category;
            byCode.UpdatedAt = DateTime.Now;
            return byCode;
        }

        var item = new Item
        {
            Code = line.ItemCode,
            Name = line.ItemName,
            MeasurementUnit = line.MeasurementUnit,
            Category = line.Category,
            CreatedAt = DateTime.Now
        };
        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    private async Task<int> GetDefaultSupplierIdAsync()
    {
        if (await _dbContext.Suppliers.AnyAsync(s => s.Id == PreferredDefaultSupplierId))
            return PreferredDefaultSupplierId;

        var existingSupplierId = await _dbContext.Suppliers
            .OrderBy(s => s.Id)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();

        if (existingSupplierId > 0)
            return existingSupplierId;

        var supplier = new Supplier
        {
            Name = "Nhà cung cấp mặc định",
            Phone = "",
            Fax = "",
            Mobile = "",
            Email = "",
            Website = "",
            CreatedAt = DateTime.Now
        };
        _dbContext.Suppliers.Add(supplier);
        await _dbContext.SaveChangesAsync();
        return supplier.Id;
    }

    private async Task ApplyStockIncreaseAsync(int itemId, int quantity, DateTime productionDate, DateTime expirationDate)
    {
        var stock = _dbContext.StockItems.Local
            .FirstOrDefault(s => s.WarehouseId == WarehouseConstants.DefaultWarehouseId && s.ItemId == itemId)
            ?? await _dbContext.StockItems
                .FirstOrDefaultAsync(s => s.WarehouseId == WarehouseConstants.DefaultWarehouseId && s.ItemId == itemId);

        if (stock == null)
        {
            _dbContext.StockItems.Add(new StockItem
            {
                WarehouseId = WarehouseConstants.DefaultWarehouseId,
                ItemId = itemId,
                Quantity = quantity,
                ProductionDate = productionDate,
                ExpirationDate = expirationDate,
                CreatedAt = DateTime.Now
            });
        }
        else
        {
            stock.Quantity += quantity;
            stock.ProductionDate = productionDate;
            stock.ExpirationDate = expirationDate;
            stock.UpdatedAt = DateTime.Now;
        }
    }

    private async Task ApplyStockDecreaseAsync(int itemId, int quantity)
    {
        var stock = _dbContext.StockItems.Local
            .FirstOrDefault(s => s.WarehouseId == WarehouseConstants.DefaultWarehouseId && s.ItemId == itemId)
            ?? await _dbContext.StockItems
                .FirstOrDefaultAsync(s => s.WarehouseId == WarehouseConstants.DefaultWarehouseId && s.ItemId == itemId)
            ?? throw new InvalidOperationException("Không tìm thấy tồn kho để hoàn tác.");

        if (stock.Quantity < quantity)
            throw new InvalidOperationException("Tồn kho không đủ để hoàn tác phiếu nhập.");

        stock.Quantity -= quantity;
        stock.UpdatedAt = DateTime.Now;
    }

    private static string GenerateOrderNumber(string prefix) =>
        $"{prefix}-{DateTime.Now:yyyyMMdd-HHmmss}";
}
