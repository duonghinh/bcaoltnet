using AspNetCore.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // Thêm thư viện này để xử lý file và đường dẫn
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Presenation.Forms
{
    public partial class WarehouseStateReportForm : Form
    {
        private readonly WarehouseService _warehouseService;
        private readonly IUnitOfWork _unitOfWork;

        public WarehouseStateReportForm()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork(new WMSDbContext());
            _warehouseService = new WarehouseService(_unitOfWork);
            LoadWarehouses();
        }

        private async Task LoadWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            comboBoxWarehouses.DataSource = warehouses;
            comboBoxWarehouses.DisplayMember = "Name";
            comboBoxWarehouses.ValueMember = "Id";
        }

        private async void btnGenerateReport_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxWarehouses.SelectedValue == null)
                {
                    MessageBox.Show("Please select a warehouse first.");
                    return;
                }

                int warehouseId = (int)comboBoxWarehouses.SelectedValue;

                // --- ĐOẠN SỬA ĐƯỜNG DẪN TẠI ĐÂY ---
                // Xóa đường dẫn tuyệt đối ổ E, dùng Path.Combine để tự tìm trong thư mục chạy App
                string reportPath = Path.Combine(Application.StartupPath, "Reports", "WarehouseStatusReport.rdlc");

                // Kiểm tra xem file có thực sự tồn tại ở đó không để báo lỗi cho chuẩn
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show($"Không tìm thấy file thiết kế báo cáo tại: {reportPath}\nBạn hãy kiểm tra xem đã có thư mục Reports trong bin\\Debug chưa.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LocalReport report = new LocalReport(reportPath);
                // ----------------------------------

                // Get the data from the service
                DataTable dataTable = await _warehouseService.GetWarehouseStatusReport(warehouseId);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for this warehouse.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Add the data source to the report
                report.AddDataSource("WarehouseStatusDataSet", dataTable);

                // Render the report to PDF
                var result = report.Execute(RenderType.Pdf);

                // Save the PDF file
                string pdfPath = Path.Combine(Application.StartupPath, $"WarehouseStatusReport_{warehouseId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
                File.WriteAllBytes(pdfPath, result.MainStream);

                MessageBox.Show($"Report generated successfully!\nSaved at: {pdfPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}