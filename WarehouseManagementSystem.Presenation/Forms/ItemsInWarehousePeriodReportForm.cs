using AspNetCore.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // Thêm thư viện này để dùng Path và File
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Data.Models;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Presenation.Forms
{
    public partial class ItemsInWarehousePeriodReportForm : Form
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly WarehouseService _warehouseService;
        public ItemsInWarehousePeriodReportForm()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork(new Data.WMSDbContext());
            _warehouseService = new WarehouseService(_unitOfWork);

            LoadWarehouses();
        }

        private async Task LoadWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();

            cmbWarehouses.DataSource = warehouses;
            cmbWarehouses.DisplayMember = "Name";
            cmbWarehouses.ValueMember = "Id";
        }

        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected warehouse ID
                if (cmbWarehouses.SelectedItem is not Warehouse selectedWarehouse)
                {
                    MessageBox.Show("Please select a warehouse.");
                    return;
                }

                // Get the date range from the UI
                DateTime fromDate = dtpFromDate.Value;
                DateTime toDate = dtpToDate.Value;

                // Get the data from the service
                var dataTable = await _warehouseService.GetItemsInWarehouseForPeriod(selectedWarehouse.Id, fromDate, toDate);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No items found for the selected warehouse and period.");
                    return;
                }

                // --- SỬA ĐƯỜNG DẪN Ở ĐÂY ---
                // Dùng Path.Combine để tự động nối thư mục chạy app với thư mục Reports
                string reportPath = Path.Combine(Application.StartupPath, "Reports", "ItemsInWarehouseForPeriodReport.rdlc");

                // Kiểm tra file tồn tại trước khi load để tránh crash
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show($"Không tìm thấy file mẫu báo cáo tại: {reportPath}\nBạn hãy kiểm tra lại thư mục bin\\Debug\\Reports", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LocalReport report = new LocalReport(reportPath);
                // ---------------------------

                // Add the data source to the report
                report.AddDataSource("ItemsInWarehouseForPeriodDataSet", dataTable);

                // Render the report to PDF
                var result = report.Execute(RenderType.Pdf);

                // Save the PDF file
                string pdfPath = Path.Combine(Application.StartupPath, $"ItemsInWarehouseForPeriodReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
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