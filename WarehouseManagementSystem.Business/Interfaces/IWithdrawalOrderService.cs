using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Business.Interfaces;

public interface IWithdrawalOrderService
{
    Task<List<OrderSummaryDto>> GetOrderSummariesAsync();
    Task<OrderInvoiceDto?> GetInvoiceAsync(int orderId);
    Task<int> CreateOrderAsync(DateTime orderDate, string recipientName, List<WithdrawalOrderLineInputDto> lines);
    Task UpdateOrderAsync(int orderId, DateTime orderDate, string recipientName, List<WithdrawalOrderLineInputDto> lines);
    Task DeleteOrderAsync(int orderId);
}
