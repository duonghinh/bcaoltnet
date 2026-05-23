using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;
using WarehouseManagementSystem.Presenation.Helpers;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class ItemsForm : Form
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ItemService _itemService;
    private int? _selectedItemId;

    public ItemsForm()
    {
        InitializeComponent();
        _unitOfWork = new UnitOfWork(new WMSDbContext());
        _itemService = new ItemService(_unitOfWork);

        if (!AppSession.CanEditItems)
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }

    private async Task LoadItems()
    {
        var keyword = txtSearch.Text.Trim();
        var category = cmbFilterCategory.SelectedItem?.ToString();
        var items = await _itemService.SearchItemsAsync(
            string.IsNullOrEmpty(keyword) ? null : keyword,
            category);

        dgvItems.DataSource = items;
        dgvItems.Columns["ItemId"].Visible = false;
        if (dgvItems.Columns.Contains("Units"))
            dgvItems.Columns["Units"].Visible = false;
        if (dgvItems.Columns.Contains("WarehouseName"))
            dgvItems.Columns["WarehouseName"].Visible = false;
        dgvItems.ReadOnly = true;
        ApplyItemGridHeaders();
    }

    private void ApplyItemGridHeaders()
    {
        SetHeader("ItemCode", "Mã hàng");
        SetHeader("ItemName", "Tên hàng");
        SetHeader("Category", "Nhóm hàng");
        SetHeader("MeasurementUnit", "ĐVT");
        SetHeader("Quantity", "Số lượng");
        SetHeader("ProductionDate", "Ngày SX");
        SetHeader("ExpirationDate", "Hạn SD");
    }

    private void SetHeader(string col, string text)
    {
        if (dgvItems.Columns.Contains(col))
            dgvItems.Columns[col].HeaderText = text;
    }

    private void ResetFormInput()
    {
        _selectedItemId = null;
        txtName.Clear();
        txtCode.Clear();
        txtQuantity.Clear();
    }

    private async Task FillFromGrid(int itemId)
    {
        _selectedItemId = itemId;
        var item = await _itemService.GetItemByIdAsync(itemId);
        txtCode.Text = item.Code;
        txtName.Text = item.Name;
        var unitIndex = cmbUnit.FindStringExact(item.MeasurementUnit);
        if (unitIndex >= 0) cmbUnit.SelectedIndex = unitIndex;
        var catIndex = cmbCategory.FindStringExact(item.Category ?? "Khác");
        cmbCategory.SelectedIndex = catIndex >= 0 ? catIndex : 0;

        var stockList = await _itemService.GetItemsByWarehouseIdAsync(WarehouseConstants.DefaultWarehouseId);
        var stock = stockList.FirstOrDefault(s => s.ItemId == itemId);
        if (stock != null)
        {
            txtQuantity.Text = stock.Quantity.ToString();
            dtpProduction.Value = stock.ProductionDate == default ? DateTime.Today : stock.ProductionDate;
            dtpExpiration.Value = stock.ExpirationDate == default ? DateTime.Today.AddYears(1) : stock.ExpirationDate;
        }
    }

    private async void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập tên và mã hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await _itemService.AddNewItemToWarehouseAsync(
                txtCode.Text.Trim(),
                txtName.Text.Trim(),
                WarehouseConstants.DefaultWarehouseId,
                quantity,
                cmbUnit.Text,
                cmbCategory.Text);

            MessageBox.Show("Thêm hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadItems();
            ResetFormInput();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!_selectedItemId.HasValue)
        {
            MessageBox.Show("Chọn hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            await _itemService.UpdateItemAsync(_selectedItemId.Value, txtCode.Text.Trim(), txtName.Text.Trim(), cmbUnit.Text, cmbCategory.Text);

            if (int.TryParse(txtQuantity.Text, out int qty))
                await _itemService.UpdateStockAsync(_selectedItemId.Value, qty, dtpProduction.Value, dtpExpiration.Value);

            MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadItems();
            ResetFormInput();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (!_selectedItemId.HasValue) return;
        if (MessageBox.Show("Xóa mặt hàng này khỏi kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        try
        {
            await _itemService.DeleteItemAsync(_selectedItemId.Value);
            MessageBox.Show("Đã xóa.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadItems();
            ResetFormInput();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void dgvItems_Click(object sender, EventArgs e)
    {
        if (dgvItems.SelectedRows.Count > 0)
        {
            int id = Convert.ToInt32(dgvItems.SelectedRows[0].Cells["ItemId"].Value);
            await FillFromGrid(id);
        }
    }

    private async void btnSearch_Click(object sender, EventArgs e) => await LoadItems();

    private void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            var folder = Path.Combine(Application.StartupPath, "Exports");
            var path = DataGridExcelExporter.ExportDataGridView(dgvItems, folder,
                $"Danh_muc_hang_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            MessageBox.Show($"Đã xuất Excel:\n{path}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void ItemsForm_Load(object sender, EventArgs e)
    {
        var units = new[] { "Cái", "Kg", "Lít", "Mét" };
        cmbUnit.Items.AddRange(units);
        var categories = new[] { "(Tất cả)", "Khác", "Thực phẩm", "Điện tử", "Văn phòng", "Hóa chất" };
        cmbFilterCategory.Items.AddRange(categories);
        cmbCategory.Items.AddRange(categories.Skip(1).ToArray());
        cmbFilterCategory.SelectedIndex = 0;
        cmbCategory.SelectedIndex = 0;
        cmbUnit.SelectedIndex = 0;
        dtpProduction.Value = DateTime.Today;
        dtpExpiration.Value = DateTime.Today.AddYears(1);
        await LoadItems();
    }
}
