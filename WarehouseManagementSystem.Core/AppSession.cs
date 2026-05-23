using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Core;

public static class AppSession
{
    public static UserSessionDto? Current { get; private set; }

    public static void SignIn(UserSessionDto session) => Current = session;

    public static void SignOut() => Current = null;

    public static bool IsSignedIn => Current != null;

    public static bool IsAdmin => Current?.RoleName == AppRoles.Admin;

    public static bool CanManageUsers => IsAdmin;

    public static bool CanEditWarehouse => IsAdmin;

    public static bool CanManageOrders =>
        IsAdmin || Current?.RoleName == AppRoles.Staff;

    public static bool CanEditItems =>
        IsAdmin || Current?.RoleName == AppRoles.Staff;

    public static bool CanViewReports =>
        IsSignedIn && (IsAdmin || Current?.RoleName == AppRoles.Staff || Current?.RoleName == AppRoles.Viewer);
}
