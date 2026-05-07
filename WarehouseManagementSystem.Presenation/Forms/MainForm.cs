using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        child.TopLevel = false;
        child.FormBorderStyle = FormBorderStyle.None;
        child.Dock = DockStyle.Fill;
        panelContainer.Controls.Add(child);
        child.BringToFront();
        child.Show();
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

    private void btnSupplier_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new SupplierForm());
    }

    private void btnCustomer_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new CustomerForm());
    }

    private void btnSupplyOrder_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new SupplyOrderForm());
    }

    // Đã xóa btnWithdrawalOrder_Click
    // Đã xóa btnStockTransfer_Click

    private void btnWarehousRepot_Click(object sender, EventArgs e)
    {
        ActivateBtn(sender);
        OpenChildForm(new WarehouseStateReportForm());
    }

    // Đã xóa iconButton1_Click (Warehouses Items Report)

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
}