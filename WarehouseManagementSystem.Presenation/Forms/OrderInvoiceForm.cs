using System;
using System.Linq;
using System.Windows.Forms;
using WarehouseManagementSystem.Core.DTOs;
using WarehouseManagementSystem.Presenation.Helpers;

namespace WarehouseManagementSystem.Presenation.Forms;

public partial class OrderInvoiceForm : Form
{
    private readonly OrderInvoiceDto _invoice;

    public OrderInvoiceForm(OrderInvoiceDto invoice)
    {
        _invoice = invoice;
        InitializeComponent();
        BindInvoice();
    }

    private void BindInvoice()
    {
        Text = _invoice.Title;
        lblTitle.Text = _invoice.Title;
        lblOrderNumber.Text = _invoice.OrderNumber;
        lblOrderType.Text = _invoice.OrderType;
        lblWarehouse.Text = _invoice.WarehouseName;
        lblPartner.Text = $"{_invoice.PartnerLabel}: {_invoice.PartnerName}";
        lblOrderDate.Text = _invoice.OrderDate.ToString("dd/MM/yyyy HH:mm");
        lblCreatedAt.Text = _invoice.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
        lblUpdatedAt.Text = _invoice.UpdatedAt.HasValue
            ? _invoice.UpdatedAt.Value.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")
            : "(Chưa cập nhật)";

        dgvLines.DataSource = _invoice.Lines.Select(l => new
        {
            l.LineNumber,
            l.ItemCode,
            l.ItemName,
            l.Quantity,
            l.MeasurementUnit,
            ProductionDate = l.ProductionDate?.ToString("dd/MM/yyyy") ?? "",
            ExpirationDate = l.ExpirationDate?.ToString("dd/MM/yyyy") ?? ""
        }).ToList();
    }

    private void btnExportPdf_Click(object sender, EventArgs e)
    {
        try
        {
            var folder = Path.Combine(Application.StartupPath, "Invoices");
            var path = InvoicePdfExporter.Export(_invoice, folder);
            MessageBox.Show($"Đã xuất PDF:\n{path}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            var folder = Path.Combine(Application.StartupPath, "Invoices");
            var path = InvoiceExcelExporter.Export(_invoice, folder);
            MessageBox.Show($"Đã xuất Excel:\n{path}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnClose_Click(object sender, EventArgs e) => Close();
}
