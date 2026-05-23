namespace WarehouseManagementSystem.Presenation.Forms;

partial class ActivityLogForm
{
    private void InitializeComponent()
    {
        panelTop = new Panel();
        lblTitle = new Label();
        btnRefresh = new Button();
        btnExportExcel = new Button();
        dgvLogs = new DataGridView();
        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
        SuspendLayout();
        // 
        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 50;
        panelTop.Controls.Add(lblTitle);
        panelTop.Controls.Add(btnRefresh);
        panelTop.Controls.Add(btnExportExcel);
        // 
        lblTitle.Text = "Nhật ký thao tác (ai nhập/xuất/sửa/xóa lúc nào)";
        lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTitle.Location = new Point(10, 12);
        lblTitle.AutoSize = true;
        // 
        btnRefresh.Text = "Làm mới";
        btnRefresh.Location = new Point(650, 8);
        btnRefresh.Click += btnRefresh_Click;
        // 
        btnExportExcel.Text = "Xuất Excel";
        btnExportExcel.Location = new Point(760, 8);
        btnExportExcel.Click += btnExportExcel_Click;
        // 
        dgvLogs.Dock = DockStyle.Fill;
        dgvLogs.AllowUserToAddRows = false;
        dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        // 
        Controls.Add(dgvLogs);
        Controls.Add(panelTop);
        Name = "ActivityLogForm";
        Text = "Nhật ký";
        Load += ActivityLogForm_Load;
        panelTop.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTop;
    private Label lblTitle;
    private Button btnRefresh;
    private Button btnExportExcel;
    private DataGridView dgvLogs;
}
