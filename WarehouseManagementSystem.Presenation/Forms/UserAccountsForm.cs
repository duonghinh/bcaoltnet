using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class UserAccountsForm : Form
{
    private readonly UserService _userService;
    private List<UserAccountDto> _users = new();

    public UserAccountsForm()
    {
        InitializeComponent();
        var uow = new UnitOfWork(new WMSDbContext());
        _userService = new UserService(uow);
    }

    private async void UserAccountsForm_Load(object sender, EventArgs e)
    {
        var roles = await _userService.GetRolesAsync();
        cmbRole.DataSource = roles.Select(r => new { r.Id, r.Name }).ToList();
        cmbRole.DisplayMember = "Name";
        cmbRole.ValueMember = "Id";
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        _users = await _userService.GetAllUsersAsync();
        dgvUsers.DataSource = _users.Select(u => new
        {
            u.Id,
            u.Username,
            u.DisplayName,
            VaiTro = u.RoleName,
            HoatDong = u.IsActive ? "Có" : "Không"
        }).ToList();
        dgvUsers.ReadOnly = true;
    }

    private void dgvUsers_Click(object sender, EventArgs e)
    {
        if (dgvUsers.SelectedRows.Count == 0) return;
        var id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["Id"].Value);
        var user = _users.First(u => u.Id == id);
        txtUsername.Text = user.Username;
        txtDisplayName.Text = user.DisplayName;
        cmbRole.SelectedValue = user.RoleId;
        chkActive.Checked = user.IsActive;
        txtPassword.Clear();
        txtUsername.ReadOnly = true;
        _selectedId = id;
    }

    private int? _selectedId;

    private void btnNew_Click(object sender, EventArgs e)
    {
        _selectedId = null;
        txtUsername.Clear();
        txtUsername.ReadOnly = false;
        txtDisplayName.Clear();
        txtPassword.Clear();
        chkActive.Checked = true;
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!_selectedId.HasValue)
            {
                await _userService.CreateUserAsync(txtUsername.Text, txtPassword.Text, txtDisplayName.Text,
                    (int)cmbRole.SelectedValue);
                MessageBox.Show("Đã tạo tài khoản.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                await _userService.UpdateUserAsync(_selectedId.Value, txtDisplayName.Text,
                    (int)cmbRole.SelectedValue, chkActive.Checked,
                    string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text);
                MessageBox.Show("Đã cập nhật.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            await LoadUsersAsync();
            btnNew_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (!_selectedId.HasValue) return;
        if (MessageBox.Show("Xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;
        try
        {
            await _userService.DeleteUserAsync(_selectedId.Value);
            await LoadUsersAsync();
            btnNew_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
