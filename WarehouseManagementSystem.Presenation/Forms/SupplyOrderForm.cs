using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class SupplyOrderForm : Form
{
    private readonly UnitOfWork _unitOfWork;
    private readonly SupplyOrderService _supplyOrderService;
    private readonly ItemService _itemService;
    private readonly List<SupplyOrderLineInputDto> _pendingLines = new();
    private int? _editingOrderId;

    public SupplyOrderForm()
    {
        InitializeComponent();
        _unitOfWork = new UnitOfWork(new WMSDbContext());
        _supplyOrderService = new SupplyOrderService(_unitOfWork);
        _itemService = new ItemService(_unitOfWork);
    }

    private async void SupplyOrderForm_Load(object sender, EventArgs e)
    {
        cmbMeasurementUnit.Items.AddRange(new[] { "Cái", "Kg", "Lít", "Mét" });
        cmbCategory.Items.AddRange(new[] { "Khác", "Thực phẩm", "Điện tử", "Văn phòng", "Hóa chất" });
        cmbCategory.SelectedIndex = 0;
        dtpOrderDate.Value = DateTime.Now;
        dtpProductionDate.Value = DateTime.Today;
        dtpExpirationDate.Value = DateTime.Today.AddYears(1);
        await ReloadOrdersAsync();
        await LoadStockItemsComboAsync();
    }

    private async Task ReloadOrdersAsync()
    {
        var orders = await _supplyOrderService.GetOrderSummariesAsync();
        dgvOrders.DataSource = orders;
        if (dgvOrders.Columns.Contains("OrderId"))
            dgvOrders.Columns["OrderId"].Visible = false;
        ApplyOrderGridHeaders();
    }

    private async Task LoadStockItemsComboAsync()
    {
        var items = await _itemService.GetItemsByWarehouseIdAsync(WarehouseManagementSystem.Core.WarehouseConstants.DefaultWarehouseId);
        cmbExistingItem.DataSource = items;
        cmbExistingItem.DisplayMember = "ItemName";
        cmbExistingItem.ValueMember = "ItemId";
    }

    private void ResetEntry()
    {
        _pendingLines.Clear();
        _editingOrderId = null;
        dgvPendingLines.DataSource = null;
        lblMode.Text = "Tạo phiếu nhập mới";
        txtItemCode.Clear();
        txtItemName.Clear();
        txtItemQty.Clear();
        chkUseExisting.Checked = false;
    }

    private void btnAddLine_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtItemQty.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Nhập số lượng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SupplyOrderLineInputDto line;
            if (chkUseExisting.Checked && cmbExistingItem.SelectedItem is ItemWarehouseDto existing)
            {
                line = new SupplyOrderLineInputDto
                {
                    ItemId = existing.ItemId,
                    ItemCode = existing.ItemCode,
                    ItemName = existing.ItemName,
                    MeasurementUnit = existing.MeasurementUnit,
                    Category = existing.Category,
                    Quantity = qty,
                    ProductionDate = dtpProductionDate.Value,
                    ExpirationDate = dtpExpirationDate.Value
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtItemCode.Text) || string.IsNullOrWhiteSpace(txtItemName.Text))
                {
                    MessageBox.Show("Nhập mã và tên hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                line = new SupplyOrderLineInputDto
                {
                    ItemCode = txtItemCode.Text.Trim(),
                    ItemName = txtItemName.Text.Trim(),
                    MeasurementUnit = cmbMeasurementUnit.Text,
                    Category = cmbCategory.Text,
                    Quantity = qty,
                    ProductionDate = dtpProductionDate.Value,
                    ExpirationDate = dtpExpirationDate.Value
                };
            }

            _pendingLines.Add(line);
            RefreshPendingGrid();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefreshPendingGrid()
    {
        dgvPendingLines.DataSource = _pendingLines.Select((l, i) => new
        {
            STT = i + 1,
            l.ItemCode,
            l.ItemName,
            l.Quantity,
            l.MeasurementUnit,
            SX = l.ProductionDate.ToString("dd/MM/yyyy"),
            HSD = l.ExpirationDate.ToString("dd/MM/yyyy")
        }).ToList();
        ApplyPendingGridHeaders();
    }

    private void ApplyOrderGridHeaders()
    {
        SetHeader(dgvOrders, "OrderNumber", "Số phiếu");
        SetHeader(dgvOrders, "OrderDate", "Ngày phiếu");
        SetHeader(dgvOrders, "CreatedAt", "Ngày tạo");
        SetHeader(dgvOrders, "UpdatedAt", "Ngày sửa");
        SetHeader(dgvOrders, "OrderType", "Loại phiếu");
        SetHeader(dgvOrders, "LineCount", "Số dòng");
        SetHeader(dgvOrders, "TotalQuantity", "Tổng SL");
    }

    private void ApplyPendingGridHeaders()
    {
        SetHeader(dgvPendingLines, "ItemCode", "Mã hàng");
        SetHeader(dgvPendingLines, "ItemName", "Tên hàng");
        SetHeader(dgvPendingLines, "Quantity", "Số lượng");
        SetHeader(dgvPendingLines, "MeasurementUnit", "ĐVT");
        SetHeader(dgvPendingLines, "SX", "Ngày SX");
        SetHeader(dgvPendingLines, "HSD", "Hạn SD");
    }

    private static void SetHeader(DataGridView grid, string columnName, string headerText)
    {
        if (grid.Columns.Contains(columnName))
            grid.Columns[columnName].HeaderText = headerText;
    }

    private async void btnSaveOrder_Click(object sender, EventArgs e)
    {
        if (_pendingLines.Count == 0)
        {
            MessageBox.Show("Thêm ít nhất một dòng hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            int orderId;
            if (_editingOrderId.HasValue)
            {
                await _supplyOrderService.UpdateOrderAsync(_editingOrderId.Value, dtpOrderDate.Value, _pendingLines);
                orderId = _editingOrderId.Value;
                MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                orderId = await _supplyOrderService.CreateOrderAsync(dtpOrderDate.Value, _pendingLines);
                MessageBox.Show("Tạo phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            await ReloadOrdersAsync();
            await LoadStockItemsComboAsync();
            ResetEntry();
            await ShowInvoiceAsync(orderId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnViewInvoice_Click(object sender, EventArgs e)
    {
        if (dgvOrders.SelectedRows.Count == 0) return;
        int id = (int)dgvOrders.SelectedRows[0].Cells["OrderId"].Value;
        await ShowInvoiceAsync(id);
    }

    private async void btnExportPdf_Click(object sender, EventArgs e)
    {
        if (dgvOrders.SelectedRows.Count == 0) return;
        int id = (int)dgvOrders.SelectedRows[0].Cells["OrderId"].Value;
        var invoice = await _supplyOrderService.GetInvoiceAsync(id);
        if (invoice == null) return;
        using var f = new OrderInvoiceForm(invoice);
        f.ShowDialog();
    }

    private async Task ShowInvoiceAsync(int orderId)
    {
        var invoice = await _supplyOrderService.GetInvoiceAsync(orderId);
        if (invoice == null)
        {
            MessageBox.Show("Không tải được hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        using var form = new OrderInvoiceForm(invoice);
        form.ShowDialog();
    }

    private async void btnEditOrder_Click(object sender, EventArgs e)
    {
        if (dgvOrders.SelectedRows.Count == 0) return;
        int id = (int)dgvOrders.SelectedRows[0].Cells["OrderId"].Value;
        var invoice = await _supplyOrderService.GetInvoiceAsync(id);
        if (invoice == null) return;

        _editingOrderId = id;
        dtpOrderDate.Value = invoice.OrderDate;
        lblMode.Text = $"Sửa phiếu: {invoice.OrderNumber}";
        _pendingLines.Clear();
        foreach (var line in invoice.Lines)
        {
            _pendingLines.Add(new SupplyOrderLineInputDto
            {
                ItemId = line.ItemId > 0 ? line.ItemId : null,
                ItemCode = line.ItemCode,
                ItemName = line.ItemName,
                MeasurementUnit = line.MeasurementUnit,
                Quantity = line.Quantity,
                ProductionDate = line.ProductionDate ?? DateTime.Today,
                ExpirationDate = line.ExpirationDate ?? DateTime.Today.AddYears(1)
            });
        }
        RefreshPendingGrid();
    }

    private async void btnDeleteOrder_Click(object sender, EventArgs e)
    {
        if (dgvOrders.SelectedRows.Count == 0) return;
        int id = (int)dgvOrders.SelectedRows[0].Cells["OrderId"].Value;
        if (MessageBox.Show("Xóa phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        try
        {
            await _supplyOrderService.DeleteOrderAsync(id);
            await ReloadOrdersAsync();
            await LoadStockItemsComboAsync();
            ResetEntry();
            MessageBox.Show("Đã xóa phiếu nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnNewOrder_Click(object sender, EventArgs e) => ResetEntry();

    private void btnRemoveLine_Click(object sender, EventArgs e)
    {
        if (dgvPendingLines.SelectedRows.Count == 0) return;
        int idx = dgvPendingLines.SelectedRows[0].Index;
        if (idx >= 0 && idx < _pendingLines.Count)
        {
            _pendingLines.RemoveAt(idx);
            RefreshPendingGrid();
        }
    }

    private void chkUseExisting_CheckedChanged(object sender, EventArgs e)
    {
        bool useExisting = chkUseExisting.Checked;
        cmbExistingItem.Enabled = useExisting;
        txtItemCode.Enabled = !useExisting;
        txtItemName.Enabled = !useExisting;
        cmbMeasurementUnit.Enabled = !useExisting;
        cmbCategory.Enabled = !useExisting;
        if (useExisting && cmbExistingItem.SelectedItem is ItemWarehouseDto item)
        {
            txtItemCode.Text = item.ItemCode;
            txtItemName.Text = item.ItemName;
        }
    }
}
