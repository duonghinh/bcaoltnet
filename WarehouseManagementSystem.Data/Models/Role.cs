namespace WarehouseManagementSystem.Data.Models;

public class Role : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public ICollection<User> Users { get; set; } = new List<User>();
}
