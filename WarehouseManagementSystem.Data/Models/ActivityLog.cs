namespace WarehouseManagementSystem.Data.Models;

public class ActivityLog
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string Username { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Action { get; set; } = "";
    public string EntityType { get; set; } = "";
    public string? Reference { get; set; }
    public string Details { get; set; } = "";
    public DateTime OccurredAt { get; set; } = DateTime.Now;
}
