using FontAwesome.Sharp;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Presenation.Forms;

namespace WarehouseManagementSystem.Presenation;

public partial class MainForm : Form
{
    private IconButton currentBtn;
    private Panel leftBorderPanal;
    private Form currentChildForm;

    public MainForm()
    {
        InitializeComponent();
        leftBorderPanal = new Panel();
        leftBorderPanal.Size = new Size(7, 60);
        SidePanal.Controls.Add(leftBorderPanal);
        ApplyPermissions();
        lblUser.Text = $"{AppSession.Current?.DisplayName} ({AppSession.Current?.RoleName})";
        OpenChildForm(new DashboardForm());
        ActivateBtn(btnDashboard);
    }

    private void ApplyPermissions()
    {
        btnWarehouse.Enabled = AppSession.CanEditWarehouse;
        btnItems.Enabled = AppSession.CanEditItems;
        btnSupplyOrder.Enabled = AppSession.CanManageOrders;
        btnWithdrawalOrder.Enabled = AppSession.CanManageOrders;
        btnUsers.Visible = AppSession.CanManageUsers;
        btnWarehousRepot.Enabled = AppSession.CanViewReports;
        btnItemsInWarehousePeriodReport.Enabled = AppSession.CanViewReports;
        btnItemsCloseToExpiration.Enabled = AppSession.CanViewReports;
        btnDashboard.Enabled = AppSession.IsSignedIn;
        btnActivityLog.Enabled = AppSession.IsSignedIn;
    }

    private void ActivateBtn(object sender)
    {
        if (sender != null)
        {
            DisableBtn();
            currentBtn = (IconButton)sender;
            currentBtn.BackColor = Color.FromArgb(19, 48, 48, 48);
            currentBtn.IconColor = Color.FromArgb(192, 0, 0);
            currentBtn.TextAlign = ContentAlignment.MiddleCenter;
            currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            currentBtn.ImageAlign = ContentAlignment.MiddleRight;

            leftBorderPanal.BackColor = Color.FromArgb(192, 0, 0);
            leftBorderPanal.Location = new Point(0, currentBtn.Location.Y);
            leftBorderPanal.Visible = true;
            leftBorderPanal.BringToFront();
        }
    }

    private void DisableBtn()
    {
        if (currentBtn != null)
        {
            currentBtn.BackColor = Color.Black;
            currentBtn.IconColor = Color.WhiteSmoke;
            currentBtn.TextAlign = ContentAlignment.MiddleLeft;
            currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
            currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
        }
    }

    private void OpenChildForm(Form child)
    {
        if (currentChildForm != null)
        {
            panelContainer.Controls.Clear();
        }
        currentChildForm = child;
        child.TopLevel = false;
        child.FormBorderStyle = FormBorderStyle.None;
        child.Dock = DockStyle.Fill;
        panelContainer.Controls.Add(child);
        child.BringToFront();
        child.Show();
    }

    private void btnDashboard_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new DashboardForm());
    }

    private void btnActivityLog_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new ActivityLogForm());
    }

    private void btnUsers_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new UserAccountsForm());
    }

    private void btnWarehouse_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new WarehouseForm());
    }

    private void btnItems_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new ItemsForm());
    }

    private void btnSupplyOrder_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new SupplyOrderForm());
    }

    private void btnWithdrawalOrder_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new WithdrawalOrderForm());
    }

    private void btnWarehousRepot_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new WarehouseStateReportForm());
    }

    private void btnItemsInWarehousePeriodReport_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new ItemsInWarehousePeriodReportForm());
    }

    private void btnItemsCloseToExpiration_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new ItemsCloseToExpirationReport());
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        AppSession.SignOut();
        Application.Restart();
    }
}
