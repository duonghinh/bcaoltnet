using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;



public class WarehouseService : IWarehouseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly WMSDbContext _dbContext;

    public WarehouseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dbContext = new WMSDbContext();
    }

    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        return await _unitOfWork.Warehouses.GetAllAsync();
    }

    public async Task<Warehouse> GetWarehouseByIdAsync(int id)
    {
        return await _unitOfWork.Warehouses.GetByIdAsync(id);
    }

    public async Task AddWarehouseAsync(string name, string address, string manager)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Warehouse name cannot be empty.");

        var warehouse = new Warehouse { Name = name, Address = address, Manager = manager };
        await _unitOfWork.Warehouses.AddAsync(warehouse);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateWarehouseAsync(int id, string name, string address, string manager)
    {
        var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id);
        if (warehouse == null)
            throw new Exception("Warehouse not found.");

        warehouse.Name = name;
        warehouse.Address = address;
        warehouse.Manager = manager;

        await _unitOfWork.Warehouses.UpdateAsync(warehouse);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteWarehouseAsync(int id)
    {
        var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id);
        if (warehouse == null)
            throw new Exception("Warehouse not found.");

        await _unitOfWork.Warehouses.DeleteAsync(warehouse);
        await _unitOfWork.SaveAsync();
    }

    // reports
    public async Task<DataTable> GetWarehouseStatusReport(int warehouseId)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("ItemName");
        dataTable.Columns.Add("Quantity");
        dataTable.Columns.Add("ProductionDate");
        dataTable.Columns.Add("ExpirationDate");

        var stockItems = await _unitOfWork.StockItemRepository.GetStockItemsWithItemAsync(warehouseId);

        foreach (var item in stockItems)
        {
            dataTable.Rows.Add(
                item?.Item?.Name ?? "Unknown",
                (item?.Quantity ?? 0).ToString(),
                FormatDate(item?.ProductionDate),
                FormatDate(item?.ExpirationDate)
            );
        }

        return dataTable;
    }

    public async Task<DataTable> GetItemsReport(List<int> warehouseIds)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("WarehouseName");
        dataTable.Columns.Add("ItemName");
        dataTable.Columns.Add("Quantity");
        dataTable.Columns.Add("ProductionDate");
        dataTable.Columns.Add("ExpirationDate");

        var stockItems = await _unitOfWork.StockItems.GetAllWithIncludesAsync(si => si.Item, si => si.Warehouse);

        // Filter the stock items based on the selected warehouse IDs
        var filteredStockItems = stockItems.Where(si => warehouseIds.Contains(si.WarehouseId)).ToList();

        foreach (var item in filteredStockItems)
        {
            string warehouseName = item?.Warehouse?.Name ?? "Unknown";
            string itemName = item?.Item?.Name ?? "Unknown";
            int quantity = item?.Quantity ?? 0;
            string productionDate = FormatDate(item?.ProductionDate);
            string expirationDate = FormatDate(item?.ExpirationDate);

            dataTable.Rows.Add(warehouseName, itemName, quantity.ToString(), productionDate, expirationDate);
        }

        return dataTable;
    }

    public async Task<DataTable> GetItemsInWarehouseForPeriod(int warehouseId, DateTime fromDate, DateTime toDate)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("WarehouseName");
        dataTable.Columns.Add("ItemName");
        dataTable.Columns.Add("Quantity");
        dataTable.Columns.Add("ProductionDate");
        dataTable.Columns.Add("ExpirationDate");
        dataTable.Columns.Add("DaysInWarehouse");

        var from = fromDate.Date;
        var toExclusive = toDate.Date.AddDays(1);

        var details = await _dbContext.Set<SupplyOrderDetail>()
            .AsNoTracking()
            .Include(d => d.Item)
            .Include(d => d.SupplyOrder)
                .ThenInclude(o => o.Warehouse)
            .Where(d => d.SupplyOrder.WarehouseId == warehouseId
                && d.SupplyOrder.OrderDate >= from
                && d.SupplyOrder.OrderDate < toExclusive)
            .OrderBy(d => d.SupplyOrder.OrderDate)
            .ThenBy(d => d.Item.Name)
            .ToListAsync();

        foreach (var detail in details)
        {
            var orderDate = detail.SupplyOrder.OrderDate.Date;
            var daysInWarehouse = Math.Max(0, (DateTime.Today - orderDate).Days);

            dataTable.Rows.Add(
                detail.SupplyOrder.Warehouse?.Name ?? "Unknown",
                detail.Item?.Name ?? "Unknown",
                detail.Quantity.ToString(),
                FormatDate(detail.ProductionDate),
                FormatDate(detail.ExpirationDate),
                daysInWarehouse.ToString()
            );
        }

        return dataTable;
    }

    private static string FormatDate(DateTime? date)
    {
        if (!date.HasValue || date.Value == default)
            return "";

        return date.Value.ToString("dd/MM/yyyy");
    }



}
