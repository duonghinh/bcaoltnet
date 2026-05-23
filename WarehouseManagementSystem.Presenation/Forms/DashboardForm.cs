using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class DashboardForm : Form
{
    private readonly DashboardService _dashboardService;

    public DashboardForm()
    {
        InitializeComponent();
        var uow = new UnitOfWork(new WMSDbContext());
        _dashboardService = new DashboardService(uow);
    }

    private async void DashboardForm_Load(object sender, EventArgs e)
    {
        await RefreshDashboardAsync();
    }

    private async Task RefreshDashboardAsync()
    {
        try
        {
            var days = (int)numExpiringDays.Value;
            var data = await _dashboardService.GetDashboardAsync(days);

            lblTotalSku.Text = data.TotalSku.ToString("N0");
            lblTotalQty.Text = data.TotalQuantity.ToString("N0");
            lblSupplyToday.Text = data.SupplyOrdersToday.ToString();
            lblWithdrawalToday.Text = data.WithdrawalOrdersToday.ToString();
            lblExpiringCount.Text = data.ExpiringSoonCount.ToString();
            lblLowStockCount.Text = data.LowStockCount.ToString();

            dgvExpiring.DataSource = data.ExpiringItems.Select(x => new
            {
                x.ItemName,
                x.Quantity,
                HạnSD = x.ExpirationDate.ToString("dd/MM/yyyy"),
                CònNgày = x.DaysUntilExpiration
            }).ToList();

            dgvLowStock.DataSource = data.LowStockItems.Select(x => new
            {
                x.ItemCode,
                x.ItemName,
                x.Quantity,
                TồnTốiThiểu = x.MinStockQuantity,
                Thiếu = x.Shortage
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnRefresh_Click(object sender, EventArgs e) => await RefreshDashboardAsync();

    private void btnExportExpiringExcel_Click(object sender, EventArgs e)
    {
        try
        {
            var folder = Path.Combine(Application.StartupPath, "Exports");
            var path = Helpers.DataGridExcelExporter.ExportDataGridView(dgvExpiring, folder,
                $"Hang_sap_het_han_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            MessageBox.Show($"Đã xuất Excel:\n{path}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
