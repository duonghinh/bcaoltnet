using System.Data;
using AspNetCore.Reporting;
using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class ItemsInWarehousePeriodReportForm : Form
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly WarehouseService _warehouseService;

    public ItemsInWarehousePeriodReportForm()
    {
        InitializeComponent();
        _unitOfWork = new UnitOfWork(new Data.WMSDbContext());
        _warehouseService = new WarehouseService(_unitOfWork);
    }

    private async void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;

            var dataTable = await _warehouseService.GetItemsInWarehouseForPeriod(
                WarehouseConstants.DefaultWarehouseId, fromDate, toDate);

            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có hàng trong khoảng thời gian đã chọn.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string reportPath = Path.Combine(Application.StartupPath, "Reports", "ItemsInWarehousePeriodReport.rdlc");

            if (!File.Exists(reportPath))
            {
                MessageBox.Show($"Không tìm thấy file mẫu báo cáo tại: {reportPath}\nBạn hãy kiểm tra lại thư mục bin\\Debug\\Reports", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var report = new LocalReport(reportPath);
            report.AddDataSource("ItemsInWarehousePeriodDataSet", dataTable);

            var result = report.Execute(RenderType.Pdf);
            string pdfPath = Path.Combine(Application.StartupPath, $"ItemsInWarehousePeriodReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            File.WriteAllBytes(pdfPath, result.MainStream);

            MessageBox.Show($"Tạo báo cáo thành công!\nĐã lưu tại: {pdfPath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
