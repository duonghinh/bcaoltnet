using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;

public class ItemService : IItemService
{
    private readonly IUnitOfWork _unitOfWork;

    public ItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
        await _unitOfWork.Items.GetAllAsync();

    public async Task<List<ItemWarehouseDto>> GetItemsInWarehousesAsync() =>
        await GetItemsByWarehouseIdAsync(WarehouseConstants.DefaultWarehouseId);

    public async Task<List<ItemWarehouseDto>> GetItemsByWarehouseIdAsync(int warehouseId)
    {
        var stockItems = await _unitOfWork.StockItemRepository
            .GetQueryable()
            .Where(s => s.WarehouseId == warehouseId)
            .Include(s => s.Item)
            .Include(s => s.Warehouse)
            .ToListAsync();

        return stockItems.Select(MapToDto).ToList();
    }

    public async Task<List<ItemWarehouseDto>> SearchItemsAsync(string? keyword, string? category)
    {
        IQueryable<StockItem> query = _unitOfWork.StockItemRepository
            .GetQueryable()
            .Where(s => s.WarehouseId == WarehouseConstants.DefaultWarehouseId);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var k = keyword.Trim().ToLower();
            query = query.Where(s =>
                s.Item.Code.ToLower().Contains(k) ||
                s.Item.Name.ToLower().Contains(k));
        }

        if (!string.IsNullOrWhiteSpace(category) && category != "(Tất cả)")
        {
            query = query.Where(s => s.Item.Category == category);
        }

        var stockItems = await query
            .Include(s => s.Item)
            .Include(s => s.Warehouse)
            .ToListAsync();
        return stockItems.Select(MapToDto).ToList();
    }

    public async Task<Item> GetItemByIdAsync(int id) =>
        await _unitOfWork.Items.GetByIdAsync(id);

    public async Task AddItemAsync(string code, string name, string measurementUnit, string category, int minStockQuantity = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên hàng không được để trống.");

        var item = new Item
        {
            Code = code,
            Name = name,
            MeasurementUnit = measurementUnit,
            Category = category ?? "Khác",
            MinStockQuantity = Math.Max(0, minStockQuantity)
        };
        await _unitOfWork.Items.AddAsync(item);
        await _unitOfWork.SaveAsync();
    }

    public async Task AddNewItemToWarehouseAsync(string itemCode, string itemName, int warehouseId, int quantity, string measurementUnit, string category = "Khác", int minStockQuantity = 0)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            throw new ArgumentException("Tên hàng không được để trống.");
        if (quantity <= 0)
            throw new ArgumentException("Số lượng phải lớn hơn 0.");

        var newItem = new Item
        {
            Code = itemCode,
            Name = itemName,
            MeasurementUnit = measurementUnit,
            Category = category ?? "Khác",
            MinStockQuantity = Math.Max(0, minStockQuantity)
        };
        await _unitOfWork.Items.AddAsync(newItem);
        await _unitOfWork.SaveAsync();

        var stockItem = new StockItem
        {
            ItemId = newItem.Id,
            WarehouseId = warehouseId,
            Quantity = quantity,
            ProductionDate = DateTime.Today,
            ExpirationDate = DateTime.Today.AddYears(1)
        };
        await _unitOfWork.StockItemRepository.AddAsync(stockItem);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateItemAsync(int id, string code, string name, string measurementUnit, string category, int minStockQuantity = 0)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(id)
            ?? throw new Exception("Không tìm thấy hàng.");

        item.Code = code;
        item.Name = name;
        item.MeasurementUnit = measurementUnit;
        item.Category = category ?? "Khác";
        item.MinStockQuantity = Math.Max(0, minStockQuantity);
        item.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Items.UpdateAsync(item);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateStockAsync(int itemId, int quantity, DateTime productionDate, DateTime expirationDate)
    {
        if (quantity < 0)
            throw new ArgumentException("Số lượng không hợp lệ.");

        var stock = await _unitOfWork.StockItemRepository
            .GetByWarehouseAndItemAsync(WarehouseConstants.DefaultWarehouseId, itemId)
            ?? throw new Exception("Không tìm thấy tồn kho.");

        stock.Quantity = quantity;
        stock.ProductionDate = productionDate;
        stock.ExpirationDate = expirationDate;
        stock.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.StockItemRepository.UpdateAsync(stock);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteItemAsync(int id)
    {
        await using var db = new WMSDbContext();

        var hasSupply = await db.Set<SupplyOrderDetail>().AnyAsync(d => d.ItemId == id);
        var hasWithdrawal = await db.Set<WithdrawalOrderDetail>().AnyAsync(d => d.ItemId == id);
        if (hasSupply || hasWithdrawal)
            throw new InvalidOperationException("Không thể xóa hàng đã có trong phiếu nhập hoặc phiếu xuất.");

        var item = await _unitOfWork.Items.GetByIdAsync(id)
            ?? throw new Exception("Không tìm thấy hàng.");

        var stock = await _unitOfWork.StockItemRepository
            .GetByWarehouseAndItemAsync(WarehouseConstants.DefaultWarehouseId, id);
        if (stock != null)
        {
            await _unitOfWork.StockItemRepository.DeleteAsync(stock);
            await _unitOfWork.SaveAsync();
        }

        await _unitOfWork.Items.DeleteAsync(item);
        await _unitOfWork.SaveAsync();

        await ActivityLogger.LogAsync(db, "Xóa", "Mặt hàng", $"Xóa hàng {item.Code} - {item.Name}", item.Code);
    }

    public async Task<DataTable> GetItemsCloseToExpirationAsync(int daysThreshold)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("ItemName");
        dataTable.Columns.Add("WarehouseName");
        dataTable.Columns.Add("Quantity");
        dataTable.Columns.Add("ProductionDate");
        dataTable.Columns.Add("ExpirationDate");
        dataTable.Columns.Add("DaysUntilExpiration");

        var currentDate = DateTime.Now;
        var stockItems = await _unitOfWork.StockItemRepository
            .GetAllWithIncludesAsync(si => si.Item, si => si.Warehouse);

        var filteredItems = stockItems
            .Where(si => si.WarehouseId == WarehouseConstants.DefaultWarehouseId
                && si.ExpirationDate != default
                && (si.ExpirationDate - currentDate).TotalDays <= daysThreshold)
            .ToList();

        foreach (var item in filteredItems)
        {
            dataTable.Rows.Add(
                item.Item.Name,
                item.Warehouse.Name,
                item.Quantity,
                item.ProductionDate.ToShortDateString(),
                item.ExpirationDate.ToShortDateString(),
                (item.ExpirationDate - currentDate).Days);
        }

        return dataTable;
    }

    private static ItemWarehouseDto MapToDto(StockItem s) => new()
    {
        ItemId = s.Item.Id,
        ItemName = s.Item.Name,
        WarehouseName = s.Warehouse.Name,
        Quantity = s.Quantity,
        ItemCode = s.Item.Code,
        Category = s.Item.Category ?? "Khác",
        MeasurementUnit = s.Item.MeasurementUnit ?? "",
        ProductionDate = s.ProductionDate,
        ExpirationDate = s.ExpirationDate,
        MinStockQuantity = s.Item.MinStockQuantity
    };
}
