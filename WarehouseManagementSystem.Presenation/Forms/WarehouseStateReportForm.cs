using System.Data;
using AspNetCore.Reporting;
using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class WarehouseStateReportForm : Form
{
    private readonly WarehouseService _warehouseService;
    private readonly IUnitOfWork _unitOfWork;

    public WarehouseStateReportForm()
    {
        InitializeComponent();
        _unitOfWork = new UnitOfWork(new WMSDbContext());
        _warehouseService = new WarehouseService(_unitOfWork);
    }

    private async void btnGenerateReport_Click_1(object sender, EventArgs e)
    {
        try
        {
            string reportPath = Path.Combine(Application.StartupPath, "Reports", "WarehouseStatusReport.rdlc");

            if (!File.Exists(reportPath))
            {
                MessageBox.Show($"Không tìm thấy file thiết kế báo cáo tại: {reportPath}\nBạn hãy kiểm tra xem đã có thư mục Reports trong bin\\Debug chưa.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dataTable = await _warehouseService.GetWarehouseStatusReport(WarehouseConstants.DefaultWarehouseId);

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu tồn kho.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var report = new LocalReport(reportPath);
            report.AddDataSource("WarehouseStatusDataSet", dataTable);

            var result = report.Execute(RenderType.Pdf);
            string pdfPath = Path.Combine(Application.StartupPath, $"WarehouseStatusReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            File.WriteAllBytes(pdfPath, result.MainStream);

            MessageBox.Show($"Tạo báo cáo thành công!\nĐã lưu tại: {pdfPath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
