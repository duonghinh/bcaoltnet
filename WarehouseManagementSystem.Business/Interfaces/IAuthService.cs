using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Business.Interfaces;

public interface IAuthService
{
    Task<UserSessionDto?> LoginAsync(string username, string password);
}
