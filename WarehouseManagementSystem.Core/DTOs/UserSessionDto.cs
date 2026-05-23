namespace WarehouseManagementSystem.Core.DTOs;

public class UserSessionDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string RoleName { get; set; } = "";
}
