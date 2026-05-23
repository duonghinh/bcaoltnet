using WarehouseManagementSystem.Business.Services;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Data;
using WarehouseManagementSystem.Data.UOW;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class WithdrawalOrderForm : Form
{
    private readonly UnitOfWork _unitOfWork;
    private readonly WithdrawalOrderService _withdrawalOrderService;
    private readonly ItemService _itemService;
    private readonly List<WithdrawalOrderLineInputDto> _pendingLines = new();
    private int? _editingOrderId;

    public WithdrawalOrderForm()
    {
        InitializeComponent();
        _unitOfWork = new UnitOfWork(new WMSDbContext());
        _withdrawalOrderService = new WithdrawalOrderService(_unitOfWork);
        _itemService = new ItemService(_unitOfWork);
    }

    private async void WithdrawalOrderForm_Load(object sender, EventArgs e)
    {
        dtpOrderDate.Value = DateTime.Now;
        await ReloadOrdersAsync();
        await LoadStockItemsComboAsync();
    }

    private async Task ReloadOrdersAsync()
    {
        var orders = await _withdrawalOrderService.GetOrderSummariesAsync();
        dgvOrders.DataSource = orders;
        if (dgvOrders.Columns.Contains("OrderId"))
            dgvOrders.Columns["OrderId"].Visible = false;
    }

    private async Task LoadStockItemsComboAsync()
    {
        var items = await _itemService.GetItemsByWarehouseIdAsync(WarehouseManagementSystem.Core.WarehouseConstants.DefaultWarehouseId);
        cmbItem.DataSource = items.Where(i => i.Quantity > 0).ToList();
        cmbItem.DisplayMember = "ItemName";
        cmbItem.ValueMember = "ItemId";
    }

    private void ResetEntry()
    {
        _pendingLines.Clear();
        _editingOrderId = null;
        dgvPendingLines.DataSource = null;
        lblMode.Text = "Tạo phiếu xuất mới";
        txtRecipient.Clear();
        txtQty.Clear();
    }

    private void btnAddLine_Click(object sender, EventArgs e)
    {
        if (cmbItem.SelectedItem is not ItemWarehouseDto item)
        {
            MessageBox.Show("Chọn hàng xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!int.TryParse(txtQty.Text, out int qty) || qty <= 0)
        {
            MessageBox.Show("Nhập số lượng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (qty > item.Quantity)
        {
            MessageBox.Show($"Tồn kho chỉ còn {item.Quantity}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _pendingLines.Add(new WithdrawalOrderLineInputDto
        {
            ItemId = item.ItemId,
            ItemCode = item.ItemCode,
            ItemName = item.ItemName,
            MeasurementUnit = item.MeasurementUnit,
            Quantity = qty
        });
        RefreshPendingGrid();
    }

    private void RefreshPendingGrid()
    {
        dgvPendingLines.DataSource = _pendingLines.Select((l, i) => new
        {
            STT = i + 1,
            l.ItemCode,
            l.ItemName,
            l.Quantity,
            l.MeasurementUnit
        }).ToList();
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
                await _withdrawalOrderService.UpdateOrderAsync(_editingOrderId.Value, dtpOrderDate.Value, txtRecipient.Text, _pendingLines);
                orderId = _editingOrderId.Value;
                MessageBox.Show("Cập nhật phiếu xuất thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                orderId = await _withdrawalOrderService.CreateOrderAsync(dtpOrderDate.Value, txtRecipient.Text, _pendingLines);
                MessageBox.Show("Tạo phiếu xuất thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        var invoice = await _withdrawalOrderService.GetInvoiceAsync(id);
        if (invoice == null) return;
        using var f = new OrderInvoiceForm(invoice);
        f.ShowDialog();
    }

    private async Task ShowInvoiceAsync(int orderId)
    {
        var invoice = await _withdrawalOrderService.GetInvoiceAsync(orderId);
        if (invoice == null) return;
        using var form = new OrderInvoiceForm(invoice);
        form.ShowDialog();
    }

    private async void btnEditOrder_Click(object sender, EventArgs e)
    {
        if (dgvOrders.SelectedRows.Count == 0) return;
        int id = (int)dgvOrders.SelectedRows[0].Cells["OrderId"].Value;
        var invoice = await _withdrawalOrderService.GetInvoiceAsync(id);
        if (invoice == null) return;

        _editingOrderId = id;
        dtpOrderDate.Value = invoice.OrderDate;
        txtRecipient.Text = invoice.PartnerName;
        lblMode.Text = $"Sửa phiếu: {invoice.OrderNumber}";
        _pendingLines.Clear();
        foreach (var line in invoice.Lines)
        {
            _pendingLines.Add(new WithdrawalOrderLineInputDto
            {
                ItemId = line.ItemId,
                ItemCode = line.ItemCode,
                ItemName = line.ItemName,
                MeasurementUnit = line.MeasurementUnit,
                Quantity = line.Quantity
            });
        }
        RefreshPendingGrid();
    }

    private async void btnDeleteOrder_Click(object sender, EventArgs e)
    {
        if (dgvOrders.SelectedRows.Count == 0) return;
        int id = (int)dgvOrders.SelectedRows[0].Cells["OrderId"].Value;
        if (MessageBox.Show("Xóa phiếu xuất này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        try
        {
            await _withdrawalOrderService.DeleteOrderAsync(id);
            await ReloadOrdersAsync();
            await LoadStockItemsComboAsync();
            ResetEntry();
            MessageBox.Show("Đã xóa phiếu xuất.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbItem.SelectedItem is ItemWarehouseDto item)
            lblStockInfo.Text = $"Tồn: {item.Quantity} {item.MeasurementUnit}";
    }
}
