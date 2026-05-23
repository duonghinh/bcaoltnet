using System.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class ItemsCloseToExpirationReport : Form
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ItemService _itemService;

    public ItemsCloseToExpirationReport()
    {
        InitializeComponent();
        _unitOfWork = new UnitOfWork(new Data.WMSDbContext());
        _itemService = new ItemService(_unitOfWork);
    }

    private async void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            int daysThreshold = (int)nudDaysThreshold.Value;

            if (daysThreshold <= 0)
            {
                MessageBox.Show("Vui lòng nhập số ngày hợp lệ.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dataTable = await _itemService.GetItemsCloseToExpirationAsync(daysThreshold);

            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có hàng sắp hết hạn.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string pdfPath = Path.Combine(Application.StartupPath, $"ItemsCloseToExpirationReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            ExportPdf(dataTable, daysThreshold, pdfPath);

            MessageBox.Show($"Tạo báo cáo thành công!\nĐã lưu tại: {pdfPath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            MessageBox.Show($"Lỗi khi tạo báo cáo: {message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void ExportPdf(DataTable dataTable, int daysThreshold, string pdfPath)
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Column(col =>
                {
                    col.Item().Text("Báo cáo hàng sắp hết hạn").Bold().FontSize(18);
                    col.Item().PaddingTop(4).Text($"Ngưỡng cảnh báo: {daysThreshold} ngày");
                    col.Item().Text($"Ngày lập: {DateTime.Now:dd/MM/yyyy HH:mm}");
                });

                page.Content().PaddingTop(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2.4f);
                        columns.RelativeColumn(2.2f);
                        columns.RelativeColumn(1f);
                        columns.RelativeColumn(1.3f);
                        columns.RelativeColumn(1.3f);
                        columns.RelativeColumn(1.2f);
                    });

                    table.Header(header =>
                    {
                        AddHeader(header, "Tên hàng");
                        AddHeader(header, "Tên kho");
                        AddHeader(header, "Số lượng");
                        AddHeader(header, "Ngày SX");
                        AddHeader(header, "Hạn SD");
                        AddHeader(header, "Còn ngày");
                    });

                    foreach (DataRow row in dataTable.Rows)
                    {
                        AddCell(table, row["ItemName"]);
                        AddCell(table, row["WarehouseName"]);
                        AddCell(table, row["Quantity"]);
                        AddCell(table, row["ProductionDate"]);
                        AddCell(table, row["ExpirationDate"]);
                        AddCell(table, row["DaysUntilExpiration"]);
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Trang ");
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        }).GeneratePdf(pdfPath);
    }

    private static void AddHeader(TableCellDescriptor table, string text)
    {
        table.Cell().Background(Colors.Grey.Lighten2).Border(1).Padding(5).Text(text).Bold();
    }

    private static void AddCell(TableDescriptor table, object? value)
    {
        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(value?.ToString() ?? "");
    }
}
