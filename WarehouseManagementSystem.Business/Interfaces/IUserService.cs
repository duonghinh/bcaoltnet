using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Business.Interfaces;

public interface IUserService
{
    Task<List<UserAccountDto>> GetAllUsersAsync();
    Task<List<(int Id, string Name)>> GetRolesAsync();
    Task CreateUserAsync(string username, string password, string displayName, int roleId);
    Task UpdateUserAsync(int id, string displayName, int roleId, bool isActive, string? newPassword);
    Task DeleteUserAsync(int id);
}
