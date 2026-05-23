using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class LoginForm : Form
{
    private readonly AuthService _authService;

    public LoginForm()
    {
        InitializeComponent();
        var uow = new UnitOfWork(new WMSDbContext());
        _authService = new AuthService(uow);
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            btnLogin.Enabled = false;
            var session = await _authService.LoginAsync(txtUsername.Text, txtPassword.Text);
            if (session == null)
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppSession.SignIn(session);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLogin.Enabled = true;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
