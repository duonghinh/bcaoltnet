namespace WarehouseManagementSystem.Core.DTOs;

public class DashboardDto
{
    public int TotalSku { get; set; }
    public int TotalQuantity { get; set; }
    public int SupplyOrdersToday { get; set; }
    public int WithdrawalOrdersToday { get; set; }
    public int LowStockCount { get; set; }
    public int ExpiringSoonCount { get; set; }
    public int ExpiringDaysThreshold { get; set; } = 30;
    public List<LowStockAlertDto> LowStockItems { get; set; } = new();
    public List<ExpiringItemAlertDto> ExpiringItems { get; set; } = new();
}

public class LowStockAlertDto
{
    public int ItemId { get; set; }
    public string ItemCode { get; set; } = "";
    public string ItemName { get; set; } = "";
    public int Quantity { get; set; }
    public int MinStockQuantity { get; set; }
    public int Shortage { get; set; }
}

public class ExpiringItemAlertDto
{
    public string ItemName { get; set; } = "";
    public int Quantity { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int DaysUntilExpiration { get; set; }
}
