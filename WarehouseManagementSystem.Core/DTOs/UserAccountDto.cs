namespace WarehouseManagementSystem.Core.DTOs;

public class UserAccountDto
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string RoleName { get; set; } = "";
    public int RoleId { get; set; }
    public bool IsActive { get; set; }
}
