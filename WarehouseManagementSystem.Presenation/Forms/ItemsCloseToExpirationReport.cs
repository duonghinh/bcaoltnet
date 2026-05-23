using System.Data;
using AspNetCore.Reporting;
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

            string reportPath = Path.Combine(Application.StartupPath, "Reports", "ItemsCloseToExpirationReport.rdlc");

            if (!File.Exists(reportPath))
            {
                MessageBox.Show($"Không tìm thấy file báo cáo tại: {reportPath}", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var report = new LocalReport(reportPath);
            report.AddDataSource("ItemsCloseToExpirationDataSet", dataTable);

            var result = report.Execute(RenderType.Pdf);
            string pdfPath = Path.Combine(Application.StartupPath, $"ItemsCloseToExpirationReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            File.WriteAllBytes(pdfPath, result.MainStream);

            MessageBox.Show($"Tạo báo cáo thành công!\nĐã lưu tại: {pdfPath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
