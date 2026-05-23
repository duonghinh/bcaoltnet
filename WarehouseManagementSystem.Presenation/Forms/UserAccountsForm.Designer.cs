namespace WarehouseManagementSystem.Presenation.Forms;

partial class UserAccountsForm
{
    private void InitializeComponent()
    {
        split = new SplitContainer();
        dgvUsers = new DataGridView();
        panelEdit = new Panel();
        lblTitle = new Label();
        lblUser = new Label();
        txtUsername = new TextBox();
        lblPass = new Label();
        txtPassword = new TextBox();
        lblName = new Label();
        txtDisplayName = new TextBox();
        lblRole = new Label();
        cmbRole = new ComboBox();
        chkActive = new CheckBox();
        btnNew = new Button();
        btnSave = new Button();
        btnDelete = new Button();
        ((System.ComponentModel.ISupportInitialize)split).BeginInit();
        split.Panel1.SuspendLayout();
        split.Panel2.SuspendLayout();
        split.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
        panelEdit.SuspendLayout();
        SuspendLayout();
        // 
        split.Dock = DockStyle.Fill;
        split.SplitterDistance = 520;
        dgvUsers.Dock = DockStyle.Fill;
        dgvUsers.Click += dgvUsers_Click;
        split.Panel1.Controls.Add(dgvUsers);
        // 
        panelEdit.Dock = DockStyle.Fill;
        panelEdit.Padding = new Padding(15);
        lblTitle.Text = "Tài khoản & phân quyền";
        lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 10);
        lblTitle.AutoSize = true;
        lblUser.Text = "Tên đăng nhập";
        lblUser.Location = new Point(15, 45);
        txtUsername.Location = new Point(15, 68);
        txtUsername.Width = 280;
        lblPass.Text = "Mật khẩu (để trống nếu giữ)";
        lblPass.Location = new Point(15, 100);
        txtPassword.Location = new Point(15, 123);
        txtPassword.Width = 280;
        txtPassword.UseSystemPasswordChar = true;
        lblName.Text = "Họ tên hiển thị";
        lblName.Location = new Point(15, 155);
        txtDisplayName.Location = new Point(15, 178);
        txtDisplayName.Width = 280;
        lblRole.Text = "Vai trò";
        lblRole.Location = new Point(15, 210);
        cmbRole.Location = new Point(15, 233);
        cmbRole.Width = 280;
        cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        chkActive.Text = "Đang hoạt động";
        chkActive.Location = new Point(15, 270);
        chkActive.Checked = true;
        btnNew.Text = "Mới";
        btnNew.Location = new Point(15, 310);
        btnNew.Click += btnNew_Click;
        btnSave.Text = "Lưu";
        btnSave.Location = new Point(100, 310);
        btnSave.Click += btnSave_Click;
        btnDelete.Text = "Xóa";
        btnDelete.Location = new Point(185, 310);
        btnDelete.Click += btnDelete_Click;
        panelEdit.Controls.AddRange(new Control[]
        {
            lblTitle, lblUser, txtUsername, lblPass, txtPassword, lblName, txtDisplayName,
            lblRole, cmbRole, chkActive, btnNew, btnSave, btnDelete
        });
        split.Panel2.Controls.Add(panelEdit);
        // 
        Controls.Add(split);
        Name = "UserAccountsForm";
        Text = "Người dùng";
        Load += UserAccountsForm_Load;
        split.Panel1.ResumeLayout(false);
        split.Panel2.ResumeLayout(false);
        split.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
        panelEdit.ResumeLayout(false);
        ResumeLayout(false);
    }

    private SplitContainer split;
    private DataGridView dgvUsers;
    private Panel panelEdit;
    private Label lblTitle, lblUser, lblPass, lblName, lblRole;
    private TextBox txtUsername, txtPassword, txtDisplayName;
    private ComboBox cmbRole;
    private CheckBox chkActive;
    private Button btnNew, btnSave, btnDelete;
}
