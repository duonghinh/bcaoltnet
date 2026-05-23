using System;
using System.Windows.Forms;
using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Data.UOW.Interfaces;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class WarehouseForm : Form
{
    private readonly WarehouseService _warehouseService;
    private readonly IUnitOfWork _unitOfWork;

    public WarehouseForm()
    {
        InitializeComponent();
        _unitOfWork = new UnitOfWork(new WMSDbContext());
        _warehouseService = new WarehouseService(_unitOfWork);
        if (!AppSession.CanEditWarehouse)
        {
            txtName.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtManager.ReadOnly = true;
            btnSave.Enabled = false;
        }
        LoadWarehouseInfo();
    }

    private async void LoadWarehouseInfo()
    {
        var warehouse = await _warehouseService.GetWarehouseByIdAsync(WarehouseConstants.DefaultWarehouseId);
        if (warehouse == null)
        {
            MessageBox.Show("Chưa có thông tin kho. Vui lòng chạy migration/seed dữ liệu.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        txtName.Text = warehouse.Name;
        txtAddress.Text = warehouse.Address;
        txtManager.Text = warehouse.Manager;
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            await _warehouseService.UpdateWarehouseAsync(
                WarehouseConstants.DefaultWarehouseId,
                txtName.Text,
                txtAddress.Text,
                txtManager.Text);

            MessageBox.Show("Đã lưu thông tin kho!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
