namespace WarehouseManagementSystem.Data.Models;

public class User : BaseEntity
{
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public bool IsActive { get; set; } = true;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
