using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Business.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync(int expiringDaysThreshold = 30);
}
