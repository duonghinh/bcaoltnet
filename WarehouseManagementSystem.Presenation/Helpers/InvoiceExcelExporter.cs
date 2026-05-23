using ClosedXML.Excel;
using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Presenation.Helpers;

public static class InvoiceExcelExporter
{
    public static string Export(OrderInvoiceDto invoice, string folder)
    {
        Directory.CreateDirectory(folder);
        var safeName = string.Join("_", invoice.OrderNumber.Split(Path.GetInvalidFileNameChars()));
        var path = Path.Combine(folder, $"{invoice.OrderType}_{safeName}.xlsx");

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Hóa đơn");

        ws.Cell(1, 1).Value = invoice.Title;
        ws.Range(1, 1, 1, 6).Merge().Style.Font.SetBold().Font.SetFontSize(14);

        ws.Cell(3, 1).Value = "Số phiếu:";
        ws.Cell(3, 2).Value = invoice.OrderNumber;
        ws.Cell(4, 1).Value = "Loại:";
        ws.Cell(4, 2).Value = invoice.OrderType;
        ws.Cell(5, 1).Value = "Kho:";
        ws.Cell(5, 2).Value = invoice.WarehouseName;
        ws.Cell(6, 1).Value = invoice.PartnerLabel + ":";
        ws.Cell(6, 2).Value = invoice.PartnerName;
        ws.Cell(7, 1).Value = "Ngày chứng từ:";
        ws.Cell(7, 2).Value = invoice.OrderDate.ToString("dd/MM/yyyy HH:mm");
        ws.Cell(8, 1).Value = "Tạo lúc:";
        ws.Cell(8, 2).Value = invoice.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
        ws.Cell(9, 1).Value = "Cập nhật:";
        ws.Cell(9, 2).Value = invoice.UpdatedAt?.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss") ?? "(Chưa cập nhật)";

        var headerRow = 11;
        ws.Cell(headerRow, 1).Value = "STT";
        ws.Cell(headerRow, 2).Value = "Mã hàng";
        ws.Cell(headerRow, 3).Value = "Tên hàng";
        ws.Cell(headerRow, 4).Value = "SL";
        ws.Cell(headerRow, 5).Value = "ĐVT";
        ws.Cell(headerRow, 6).Value = "Ngày SX";
        ws.Cell(headerRow, 7).Value = "Hạn SD";
        ws.Range(headerRow, 1, headerRow, 7).Style.Font.SetBold();

        var row = headerRow + 1;
        foreach (var line in invoice.Lines)
        {
            ws.Cell(row, 1).Value = line.LineNumber;
            ws.Cell(row, 2).Value = line.ItemCode;
            ws.Cell(row, 3).Value = line.ItemName;
            ws.Cell(row, 4).Value = line.Quantity;
            ws.Cell(row, 5).Value = line.MeasurementUnit;
            ws.Cell(row, 6).Value = line.ProductionDate?.ToString("dd/MM/yyyy") ?? "";
            ws.Cell(row, 7).Value = line.ExpirationDate?.ToString("dd/MM/yyyy") ?? "";
            row++;
        }

        ws.Columns().AdjustToContents();
        wb.SaveAs(path);
        return path;
    }
}
