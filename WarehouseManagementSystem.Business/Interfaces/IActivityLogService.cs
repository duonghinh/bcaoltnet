using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Business.Interfaces;

public interface IActivityLogService
{
    Task LogAsync(string action, string entityType, string details, string? reference = null);
    Task<List<ActivityLogDto>> GetLogsAsync(int take = 500);
}
