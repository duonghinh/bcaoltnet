using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;

public class ActivityLogService : IActivityLogService
{
    private readonly WMSDbContext _dbContext;

    public ActivityLogService(IUnitOfWork unitOfWork)
    {
        _dbContext = new WMSDbContext();
    }

    public async Task LogAsync(string action, string entityType, string details, string? reference = null)
    {
        var session = AppSession.Current;
        _dbContext.ActivityLogs.Add(new ActivityLog
        {
            UserId = session?.UserId,
            Username = session?.Username ?? "system",
            DisplayName = session?.DisplayName ?? "Hệ thống",
            Action = action,
            EntityType = entityType,
            Reference = reference,
            Details = details,
            OccurredAt = DateTime.Now
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ActivityLogDto>> GetLogsAsync(int take = 500)
    {
        return await _dbContext.ActivityLogs
            .OrderByDescending(l => l.OccurredAt)
            .Take(take)
            .Select(l => new ActivityLogDto
            {
                Id = l.Id,
                Username = l.Username,
                DisplayName = l.DisplayName,
                Action = l.Action,
                EntityType = l.EntityType,
                Reference = l.Reference,
                Details = l.Details,
                OccurredAt = l.OccurredAt
            })
            .ToListAsync();
    }
}
