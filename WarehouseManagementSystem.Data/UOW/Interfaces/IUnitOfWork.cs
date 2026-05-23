using Microsoft.EntityFrameworkCore.Storage;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.Repositories.Interfaces;

namespace WarehouseManagementSystem.Data.UOW.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IWarehouseRepository Warehouses { get; }
    public IRepository<Item> Items { get; }
    public IStockItemRepository StockItems { get; }
    public ISupplyOrderRepository SupplyOrders { get; }
    public IRepository<SupplyOrderDetail> SupplyOrderDetails { get; }
    public IRepository<StockTransfer> StockTransfers { get; }
    public IStockItemRepository StockItemRepository { get; }
    public IWithdrawalOrderRepository WithdrawalOrders { get; }
    public IRepository<User> Users { get; }
    public IRepository<Role> Roles { get; }
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task SaveAsync();
}
