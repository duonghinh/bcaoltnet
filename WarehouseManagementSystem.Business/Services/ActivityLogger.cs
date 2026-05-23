using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Core;

namespace WarehouseManagementSystem.Business.Services;

internal static class ActivityLogger
{
    public static async Task LogAsync(WMSDbContext db, string action, string entityType, string details, string? reference = null)
    {
        var session = AppSession.Current;
        db.ActivityLogs.Add(new ActivityLog
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
        await db.SaveChangesAsync();
    }
}
