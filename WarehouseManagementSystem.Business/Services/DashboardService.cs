using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;

public class DashboardService : IDashboardService
{
    private readonly WMSDbContext _dbContext;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _dbContext = new WMSDbContext();
    }

    public async Task<DashboardDto> GetDashboardAsync(int expiringDaysThreshold = 30)
    {
        var warehouseId = WarehouseConstants.DefaultWarehouseId;
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var stockItems = await _dbContext.StockItems
            .Where(s => s.WarehouseId == warehouseId)
            .Include(s => s.Item)
            .ToListAsync();

        var supplyToday = await _dbContext.SupplyOrders
            .CountAsync(o => o.WarehouseId == warehouseId && o.OrderDate >= today && o.OrderDate < tomorrow);

        var withdrawalToday = await _dbContext.WithdrawalOrders
            .CountAsync(o => o.WarehouseId == warehouseId && o.OrderDate >= today && o.OrderDate < tomorrow);

        var now = DateTime.Now;
        var lowStock = stockItems
            .Where(s => s.Item.MinStockQuantity > 0 && s.Quantity <= s.Item.MinStockQuantity)
            .Select(s => new LowStockAlertDto
            {
                ItemId = s.ItemId,
                ItemCode = s.Item.Code,
                ItemName = s.Item.Name,
                Quantity = s.Quantity,
                MinStockQuantity = s.Item.MinStockQuantity,
                Shortage = Math.Max(0, s.Item.MinStockQuantity - s.Quantity)
            })
            .OrderBy(x => x.Quantity)
            .ToList();

        var expiring = stockItems
            .Where(s => s.ExpirationDate != default
                && (s.ExpirationDate - now).TotalDays <= expiringDaysThreshold
                && s.Quantity > 0)
            .Select(s => new ExpiringItemAlertDto
            {
                ItemName = s.Item.Name,
                Quantity = s.Quantity,
                ExpirationDate = s.ExpirationDate,
                DaysUntilExpiration = (int)(s.ExpirationDate - now).TotalDays
            })
            .OrderBy(x => x.DaysUntilExpiration)
            .ToList();

        return new DashboardDto
        {
            TotalSku = stockItems.Count(s => s.Quantity > 0),
            TotalQuantity = stockItems.Sum(s => s.Quantity),
            SupplyOrdersToday = supplyToday,
            WithdrawalOrdersToday = withdrawalToday,
            LowStockCount = lowStock.Count,
            ExpiringSoonCount = expiring.Count,
            ExpiringDaysThreshold = expiringDaysThreshold,
            LowStockItems = lowStock,
            ExpiringItems = expiring
        };
    }
}
