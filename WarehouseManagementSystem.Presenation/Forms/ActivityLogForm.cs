using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Presenation.Helpers;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class ActivityLogForm : Form
{
    private readonly ActivityLogService _logService;

    public ActivityLogForm()
    {
        InitializeComponent();
        var uow = new UnitOfWork(new WMSDbContext());
        _logService = new ActivityLogService(uow);
    }

    private async void ActivityLogForm_Load(object sender, EventArgs e) => await LoadLogsAsync();

    private async Task LoadLogsAsync()
    {
        var logs = await _logService.GetLogsAsync(1000);
        dgvLogs.DataSource = logs.Select(l => new
        {
            l.OccurredAt,
            NguoiDung = l.DisplayName,
            l.Username,
            l.Action,
            Loai = l.EntityType,
            ThamChieu = l.Reference,
            l.Details
        }).ToList();
        dgvLogs.Columns["OccurredAt"].HeaderText = "Thời gian";
        dgvLogs.Columns["NguoiDung"].HeaderText = "Họ tên";
        dgvLogs.Columns["Username"].HeaderText = "Tài khoản";
        dgvLogs.Columns["Action"].HeaderText = "Thao tác";
        dgvLogs.Columns["Loai"].HeaderText = "Đối tượng";
        dgvLogs.Columns["ThamChieu"].HeaderText = "Mã tham chiếu";
        dgvLogs.Columns["Details"].HeaderText = "Chi tiết";
        dgvLogs.ReadOnly = true;
    }

    private async void btnRefresh_Click(object sender, EventArgs e) => await LoadLogsAsync();

    private void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            var folder = Path.Combine(Application.StartupPath, "Exports");
            var path = DataGridExcelExporter.ExportDataGridView(dgvLogs, folder,
                $"Nhat_ky_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            MessageBox.Show($"Đã xuất Excel:\n{path}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
