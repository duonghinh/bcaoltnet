using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WarehouseManagementSystem.Core.DTOs;

namespace WarehouseManagementSystem.Presenation.Helpers;

public static class InvoicePdfExporter
{
    public static string Export(OrderInvoiceDto invoice, string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);
        var safeNumber = string.Join("_", invoice.OrderNumber.Split(Path.GetInvalidFileNameChars()));
        var path = Path.Combine(outputDirectory, $"{safeNumber}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Column(col =>
                {
                    col.Item().Text(invoice.Title).Bold().FontSize(18);
                    col.Item().PaddingTop(8).Text($"Số phiếu: {invoice.OrderNumber}");
                    col.Item().Text($"Loại: {invoice.OrderType}");
                    col.Item().Text($"Kho: {invoice.WarehouseName}");
                    col.Item().Text($"{invoice.PartnerLabel}: {invoice.PartnerName}");
                });

                page.Content().PaddingVertical(15).Column(col =>
                {
                    col.Item().Text("Mốc thời gian").Bold().FontSize(13);
                    col.Item().PaddingTop(5).Text($"Ngày chứng từ: {invoice.OrderDate:dd/MM/yyyy HH:mm}");
                    col.Item().Text($"Thời điểm tạo: {invoice.CreatedAt.ToLocalTime():dd/MM/yyyy HH:mm:ss}");
                    if (invoice.UpdatedAt.HasValue)
                        col.Item().Text($"Cập nhật lần cuối: {invoice.UpdatedAt.Value.ToLocalTime():dd/MM/yyyy HH:mm:ss}");

                    col.Item().PaddingTop(20).Text("Chi tiết hàng").Bold().FontSize(13);

                    col.Item().PaddingTop(8).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("STT").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("Mã").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("Tên hàng").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("SL").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("ĐVT").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("Ghi chú").Bold();
                        });

                        foreach (var line in invoice.Lines)
                        {
                            var note = "";
                            if (line.ProductionDate.HasValue)
                                note += $"SX: {line.ProductionDate:dd/MM/yyyy} ";
                            if (line.ExpirationDate.HasValue)
                                note += $"HSD: {line.ExpirationDate:dd/MM/yyyy}";

                            table.Cell().Padding(4).Text(line.LineNumber.ToString());
                            table.Cell().Padding(4).Text(line.ItemCode);
                            table.Cell().Padding(4).Text(line.ItemName);
                            table.Cell().Padding(4).Text(line.Quantity.ToString());
                            table.Cell().Padding(4).Text(line.MeasurementUnit);
                            table.Cell().Padding(4).Text(note.Trim());
                        }
                    });

                    col.Item().PaddingTop(15).AlignRight().Text($"Tổng số lượng: {invoice.Lines.Sum(l => l.Quantity)}").Bold();
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("In lúc: ");
                    text.Span($"{DateTime.Now:dd/MM/yyyy HH:mm:ss}").Bold();
                });
            });
        }).GeneratePdf(path);

        return path;
    }
}
