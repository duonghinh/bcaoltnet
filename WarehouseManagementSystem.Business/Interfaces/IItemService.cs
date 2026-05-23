using System.Data;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data.Models;

namespace WarehouseManagementSystem.Business.Interfaces;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task<Item> GetItemByIdAsync(int id);
    Task AddItemAsync(string code, string name, string measurementUnit, string category, int minStockQuantity = 0);
    Task UpdateItemAsync(int id, string code, string name, string measurementUnit, string category, int minStockQuantity = 0);
    Task AddNewItemToWarehouseAsync(string itemCode, string itemName, int warehouseId, int quantity, string measurementUnit, string category = "Khác", int minStockQuantity = 0);
    Task DeleteItemAsync(int id);
    Task<List<ItemWarehouseDto>> GetItemsByWarehouseIdAsync(int warehouseId);
    Task<List<ItemWarehouseDto>> SearchItemsAsync(string? keyword, string? category);
    Task UpdateStockAsync(int itemId, int quantity, DateTime productionDate, DateTime expirationDate);
    Task<DataTable> GetItemsCloseToExpirationAsync(int daysThreshold);
}
