using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Core.Security;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;

public class AuthService : IAuthService
{
    private readonly WMSDbContext _dbContext;

    public AuthService(IUnitOfWork unitOfWork)
    {
        _dbContext = new WMSDbContext();
    }

    public async Task<UserSessionDto?> LoginAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return null;

        var user = await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == username.Trim() && u.IsActive);

        if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
            return null;

        return new UserSessionDto
        {
            UserId = user.Id,
            Username = user.Username,
            DisplayName = user.DisplayName,
            RoleName = user.Role.Name
        };
    }
}
