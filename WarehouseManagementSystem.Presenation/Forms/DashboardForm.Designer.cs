namespace WarehouseManagementSystem.Presenation.Forms;

partial class DashboardForm
{
    private void InitializeComponent()
    {
        panelStats = new Panel();
        lblTitle = new Label();
        lblSkuCap = new Label();
        lblTotalSku = new Label();
        lblQtyCap = new Label();
        lblTotalQty = new Label();
        lblSupplyCap = new Label();
        lblSupplyToday = new Label();
        lblWithdrawCap = new Label();
        lblWithdrawalToday = new Label();
        lblExpCap = new Label();
        lblExpiringCount = new Label();
        lblLowCap = new Label();
        lblLowStockCount = new Label();
        numExpiringDays = new NumericUpDown();
        lblDays = new Label();
        btnRefresh = new Button();
        splitMain = new SplitContainer();
        lblExpiringTitle = new Label();
        dgvExpiring = new DataGridView();
        btnExportExpiringExcel = new Button();
        lblLowTitle = new Label();
        dgvLowStock = new DataGridView();
        panelStats.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numExpiringDays).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvExpiring).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLowStock).BeginInit();
        SuspendLayout();
        // 
        panelStats.Dock = DockStyle.Top;
        panelStats.Height = 160;
        panelStats.Padding = new Padding(15);
        panelStats.Controls.Add(lblTitle);
        panelStats.Controls.Add(lblSkuCap);
        panelStats.Controls.Add(lblTotalSku);
        panelStats.Controls.Add(lblQtyCap);
        panelStats.Controls.Add(lblTotalQty);
        panelStats.Controls.Add(lblSupplyCap);
        panelStats.Controls.Add(lblSupplyToday);
        panelStats.Controls.Add(lblWithdrawCap);
        panelStats.Controls.Add(lblWithdrawalToday);
        panelStats.Controls.Add(lblExpCap);
        panelStats.Controls.Add(lblExpiringCount);
        panelStats.Controls.Add(lblLowCap);
        panelStats.Controls.Add(lblLowStockCount);
        panelStats.Controls.Add(lblDays);
        panelStats.Controls.Add(numExpiringDays);
        panelStats.Controls.Add(btnRefresh);
        // 
        lblTitle.Text = "Tổng quan kho";
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 10);
        lblTitle.AutoSize = true;
        // 
        lblSkuCap.Text = "Tổng SKU (có tồn):";
        lblSkuCap.Location = new Point(15, 50);
        lblTotalSku.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotalSku.ForeColor = Color.DarkRed;
        lblTotalSku.Location = new Point(160, 48);
        lblTotalSku.Text = "0";
        // 
        lblQtyCap.Text = "Tổng số lượng:";
        lblQtyCap.Location = new Point(15, 80);
        lblTotalQty.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotalQty.ForeColor = Color.DarkRed;
        lblTotalQty.Location = new Point(160, 78);
        lblTotalQty.Text = "0";
        // 
        lblSupplyCap.Text = "Phiếu nhập hôm nay:";
        lblSupplyCap.Location = new Point(280, 50);
        lblSupplyToday.Location = new Point(440, 48);
        lblSupplyToday.Text = "0";
        // 
        lblWithdrawCap.Text = "Phiếu xuất hôm nay:";
        lblWithdrawCap.Location = new Point(280, 80);
        lblWithdrawalToday.Location = new Point(440, 78);
        lblWithdrawalToday.Text = "0";
        // 
        lblExpCap.Text = "Hàng sắp hết hạn:";
        lblExpCap.Location = new Point(550, 50);
        lblExpiringCount.ForeColor = Color.OrangeRed;
        lblExpiringCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblExpiringCount.Location = new Point(700, 48);
        // 
        lblLowCap.Text = "Hàng tồn thấp:";
        lblLowCap.Location = new Point(550, 80);
        lblLowStockCount.ForeColor = Color.OrangeRed;
        lblLowStockCount.Location = new Point(700, 78);
        // 
        lblDays.Text = "Ngưỡng HSD (ngày):";
        lblDays.Location = new Point(820, 50);
        numExpiringDays.Location = new Point(980, 48);
        numExpiringDays.Minimum = 1;
        numExpiringDays.Maximum = 365;
        numExpiringDays.Value = 30;
        numExpiringDays.Width = 60;
        // 
        btnRefresh.Text = "Làm mới";
        btnRefresh.Location = new Point(820, 75);
        btnRefresh.Click += btnRefresh_Click;
        // 
        splitMain.Dock = DockStyle.Fill;
        splitMain.Orientation = Orientation.Horizontal;
        splitMain.SplitterDistance = 280;
        // 
        lblExpiringTitle.Text = "Hàng sắp hết hạn";
        lblExpiringTitle.Dock = DockStyle.Top;
        lblExpiringTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblExpiringTitle.Height = 28;
        dgvExpiring.Dock = DockStyle.Fill;
        dgvExpiring.ReadOnly = true;
        dgvExpiring.AllowUserToAddRows = false;
        dgvExpiring.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        btnExportExpiringExcel.Text = "Xuất Excel";
        btnExportExpiringExcel.Dock = DockStyle.Bottom;
        btnExportExpiringExcel.Height = 36;
        btnExportExpiringExcel.Click += btnExportExpiringExcel_Click;
        splitMain.Panel1.Controls.Add(dgvExpiring);
        splitMain.Panel1.Controls.Add(btnExportExpiringExcel);
        splitMain.Panel1.Controls.Add(lblExpiringTitle);
        // 
        lblLowTitle.Text = "Hàng tồn dưới mức tối thiểu";
        lblLowTitle.Dock = DockStyle.Top;
        lblLowTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLowTitle.Height = 28;
        dgvLowStock.Dock = DockStyle.Fill;
        dgvLowStock.ReadOnly = true;
        dgvLowStock.AllowUserToAddRows = false;
        dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        splitMain.Panel2.Controls.Add(dgvLowStock);
        splitMain.Panel2.Controls.Add(lblLowTitle);
        // 
        Controls.Add(splitMain);
        Controls.Add(panelStats);
        Name = "DashboardForm";
        Text = "Dashboard";
        Load += DashboardForm_Load;
        panelStats.ResumeLayout(false);
        panelStats.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numExpiringDays).EndInit();
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvExpiring).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLowStock).EndInit();
        ResumeLayout(false);
    }

    private Panel panelStats;
    private Label lblTitle;
    private Label lblSkuCap, lblTotalSku, lblQtyCap, lblTotalQty;
    private Label lblSupplyCap, lblSupplyToday, lblWithdrawCap, lblWithdrawalToday;
    private Label lblExpCap, lblExpiringCount, lblLowCap, lblLowStockCount;
    private Label lblDays;
    private NumericUpDown numExpiringDays;
    private Button btnRefresh;
    private SplitContainer splitMain;
    private Label lblExpiringTitle;
    private DataGridView dgvExpiring;
    private Button btnExportExpiringExcel;
    private Label lblLowTitle;
    private DataGridView dgvLowStock;
}
