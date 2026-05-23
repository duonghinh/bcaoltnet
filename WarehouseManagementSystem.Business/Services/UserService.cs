using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Business.Interfaces;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Core.Security;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Business.Services;

public class UserService : IUserService
{
    private readonly WMSDbContext _dbContext;

    public UserService(IUnitOfWork unitOfWork)
    {
        _dbContext = new WMSDbContext();
    }

    public async Task<List<UserAccountDto>> GetAllUsersAsync()
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .OrderBy(u => u.Username)
            .Select(u => new UserAccountDto
            {
                Id = u.Id,
                Username = u.Username,
                DisplayName = u.DisplayName,
                RoleId = u.RoleId,
                RoleName = u.Role.Name,
                IsActive = u.IsActive
            })
            .ToListAsync();
    }

    public async Task<List<(int Id, string Name)>> GetRolesAsync()
    {
        var roles = await _dbContext.Roles.OrderBy(r => r.Id).ToListAsync();
        return roles.Select(r => (r.Id, r.Name)).ToList();
    }

    public async Task CreateUserAsync(string username, string password, string displayName, int roleId)
    {
        username = username.Trim();
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Tên đăng nhập không được để trống.");
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Mật khẩu không được để trống.");

        if (await _dbContext.Users.AnyAsync(u => u.Username == username))
            throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");

        _dbContext.Users.Add(new User
        {
            Username = username,
            PasswordHash = PasswordHasher.Hash(password),
            DisplayName = displayName.Trim(),
            RoleId = roleId,
            IsActive = true,
            CreatedAt = DateTime.Now
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int id, string displayName, int roleId, bool isActive, string? newPassword)
    {
        var user = await _dbContext.Users.FindAsync(id)
            ?? throw new InvalidOperationException("Không tìm thấy người dùng.");

        user.DisplayName = displayName.Trim();
        user.RoleId = roleId;
        user.IsActive = isActive;
        user.UpdatedAt = DateTime.Now;

        if (!string.IsNullOrWhiteSpace(newPassword))
            user.PasswordHash = PasswordHasher.Hash(newPassword);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        if (AppSession.Current?.UserId == id)
            throw new InvalidOperationException("Không thể xóa tài khoản đang đăng nhập.");

        var user = await _dbContext.Users.FindAsync(id)
            ?? throw new InvalidOperationException("Không tìm thấy người dùng.");

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}
