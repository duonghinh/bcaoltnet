namespace WarehouseManagementSystem.Core.DTOs;

public class ActivityLogDto
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Action { get; set; } = "";
    public string EntityType { get; set; } = "";
    public string? Reference { get; set; }
    public string Details { get; set; } = "";
    public DateTime OccurredAt { get; set; }
}
