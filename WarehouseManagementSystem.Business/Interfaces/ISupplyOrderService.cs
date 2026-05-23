using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Business.Interfaces;

public interface ISupplyOrderService
{
    Task<List<OrderSummaryDto>> GetOrderSummariesAsync();
    Task<OrderInvoiceDto?> GetInvoiceAsync(int orderId);
    Task<int> CreateOrderAsync(DateTime orderDate, List<SupplyOrderLineInputDto> lines);
    Task UpdateOrderAsync(int orderId, DateTime orderDate, List<SupplyOrderLineInputDto> lines);
    Task DeleteOrderAsync(int orderId);
}
