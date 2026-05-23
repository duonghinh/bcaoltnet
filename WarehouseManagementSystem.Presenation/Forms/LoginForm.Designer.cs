namespace WarehouseManagementSystem.Presenation.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitle = new Label();
        lblUser = new Label();
        lblPass = new Label();
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnCancel = new Button();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.Location = new Point(40, 25);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(297, 32);
        lblTitle.TabIndex = 7;
        lblTitle.Text = "Đăng nhập hệ thống kho";
        // 
        // lblUser
        // 
        lblUser.AutoSize = true;
        lblUser.Location = new Point(40, 75);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(107, 20);
        lblUser.TabIndex = 6;
        lblUser.Text = "Tên đăng nhập";
        // 
        // lblPass
        // 
        lblPass.AutoSize = true;
        lblPass.Location = new Point(40, 135);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(70, 20);
        lblPass.TabIndex = 4;
        lblPass.Text = "Mật khẩu";
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(40, 98);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(320, 27);
        txtUsername.TabIndex = 5;
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(40, 158);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(320, 27);
        txtPassword.TabIndex = 3;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // btnLogin
        // 
        btnLogin.Location = new Point(40, 210);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(150, 36);
        btnLogin.TabIndex = 2;
        btnLogin.Text = "Đăng nhập";
        btnLogin.Click += btnLogin_Click;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(210, 210);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(150, 36);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "Thoát";
        btnCancel.Click += btnCancel_Click;
        // 
        // LoginForm
        // 
        AcceptButton = btnLogin;
        CancelButton = btnCancel;
        ClientSize = new Size(400, 360);
        Controls.Add(btnCancel);
        Controls.Add(btnLogin);
        Controls.Add(txtPassword);
        Controls.Add(lblPass);
        Controls.Add(txtUsername);
        Controls.Add(lblUser);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Đăng nhập";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblTitle;
    private Label lblUser;
    private Label lblPass;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnCancel;
}
