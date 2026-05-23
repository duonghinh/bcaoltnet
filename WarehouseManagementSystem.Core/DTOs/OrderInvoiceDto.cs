namespace WarehouseManagementSystem.Core.DTOs;

public class OrderInvoiceDto
{
    public string Title { get; set; } = "";
    public string OrderType { get; set; } = "";
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string WarehouseName { get; set; } = "";
    public string PartnerLabel { get; set; } = "";
    public string PartnerName { get; set; } = "";
    public List<OrderInvoiceLineDto> Lines { get; set; } = new();
}

public class OrderInvoiceLineDto
{
    public int ItemId { get; set; }
    public int LineNumber { get; set; }
    public string ItemCode { get; set; } = "";
    public string ItemName { get; set; } = "";
    public string MeasurementUnit { get; set; } = "";
    public int Quantity { get; set; }
    public DateTime? ProductionDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
}

public class OrderSummaryDto
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string OrderType { get; set; } = "";
    public int LineCount { get; set; }
    public int TotalQuantity { get; set; }
}

public class SupplyOrderLineInputDto
{
    public int? ItemId { get; set; }
    public string ItemCode { get; set; } = "";
    public string ItemName { get; set; } = "";
    public string MeasurementUnit { get; set; } = "Cái";
    public string Category { get; set; } = "Khác";
    public int Quantity { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}

public class WithdrawalOrderLineInputDto
{
    public int ItemId { get; set; }
    public string ItemCode { get; set; } = "";
    public string ItemName { get; set; } = "";
    public string MeasurementUnit { get; set; } = "";
    public int Quantity { get; set; }
}
