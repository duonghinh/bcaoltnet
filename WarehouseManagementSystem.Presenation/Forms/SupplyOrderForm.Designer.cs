namespace WarehouseManagementSystem.Presenation.Forms
{
    partial class SupplyOrderForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvOrders = new DataGridView();
            dgvPendingLines = new DataGridView();
            dtpOrderDate = new DateTimePicker();
            chkUseExisting = new CheckBox();
            cmbExistingItem = new ComboBox();
            txtItemCode = new TextBox();
            txtItemName = new TextBox();
            txtItemQty = new TextBox();
            cmbMeasurementUnit = new ComboBox();
            cmbCategory = new ComboBox();
            dtpProductionDate = new DateTimePicker();
            dtpExpirationDate = new DateTimePicker();
            btnAddLine = new Button();
            btnRemoveLine = new Button();
            btnSaveOrder = new Button();
            btnNewOrder = new Button();
            btnViewInvoice = new Button();
            btnExportPdf = new Button();
            btnEditOrder = new Button();
            btnDeleteOrder = new Button();
            lblMode = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPendingLines).BeginInit();
            SuspendLayout();
            // 
            lblMode.AutoSize = true;
            lblMode.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMode.Location = new Point(12, 12);
            lblMode.Text = "Tạo phiếu nhập mới";
            // 
            label1.Location = new Point(12, 42);
            label1.Text = "Ngày phiếu:";
            dtpOrderDate.Location = new Point(100, 38);
            dtpOrderDate.Size = new Size(250, 27);
            // 
            dgvOrders.Location = new Point(12, 75);
            dgvOrders.Size = new Size(1350, 180);
            dgvOrders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            btnViewInvoice.Location = new Point(12, 262);
            btnViewInvoice.Text = "Xem HĐ";
            btnViewInvoice.Click += btnViewInvoice_Click;
            btnExportPdf.Location = new Point(100, 262);
            btnExportPdf.Text = "Hóa đơn + PDF";
            btnExportPdf.Click += btnExportPdf_Click;
            btnEditOrder.Location = new Point(220, 262);
            btnEditOrder.Text = "Sửa";
            btnEditOrder.Click += btnEditOrder_Click;
            btnDeleteOrder.Location = new Point(290, 262);
            btnDeleteOrder.Text = "Xóa";
            btnDeleteOrder.Click += btnDeleteOrder_Click;
            btnNewOrder.Location = new Point(360, 262);
            btnNewOrder.Text = "Phiếu mới";
            btnNewOrder.Click += btnNewOrder_Click;
            // 
            chkUseExisting.Location = new Point(12, 305);
            chkUseExisting.Text = "Chọn hàng có sẵn";
            chkUseExisting.CheckedChanged += chkUseExisting_CheckedChanged;
            cmbExistingItem.Location = new Point(180, 301);
            cmbExistingItem.Size = new Size(280, 28);
            // 
            txtItemCode.Location = new Point(12, 345);
            txtItemName.Location = new Point(220, 345);
            txtItemQty.Location = new Point(500, 345);
            txtItemQty.Width = 80;
            cmbMeasurementUnit.Location = new Point(600, 345);
            cmbCategory.Location = new Point(780, 345);
            dtpProductionDate.Location = new Point(12, 385);
            dtpExpirationDate.Location = new Point(250, 385);
            btnAddLine.Location = new Point(500, 383);
            btnAddLine.Text = "Thêm dòng";
            btnAddLine.Click += btnAddLine_Click;
            btnRemoveLine.Location = new Point(620, 383);
            btnRemoveLine.Text = "Xóa dòng";
            btnRemoveLine.Click += btnRemoveLine_Click;
            btnSaveOrder.Location = new Point(740, 383);
            btnSaveOrder.Text = "Lưu phiếu";
            btnSaveOrder.BackColor = Color.DodgerBlue;
            btnSaveOrder.ForeColor = Color.White;
            btnSaveOrder.Click += btnSaveOrder_Click;
            // 
            dgvPendingLines.Location = new Point(12, 425);
            dgvPendingLines.Size = new Size(1350, 120);
            dgvPendingLines.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvPendingLines.ReadOnly = true;
            // 
            ClientSize = new Size(1380, 560);
            Controls.Add(dgvPendingLines);
            Controls.Add(btnSaveOrder);
            Controls.Add(btnRemoveLine);
            Controls.Add(btnAddLine);
            Controls.Add(dtpExpirationDate);
            Controls.Add(dtpProductionDate);
            Controls.Add(cmbCategory);
            Controls.Add(cmbMeasurementUnit);
            Controls.Add(txtItemQty);
            Controls.Add(txtItemName);
            Controls.Add(txtItemCode);
            Controls.Add(cmbExistingItem);
            Controls.Add(chkUseExisting);
            Controls.Add(btnNewOrder);
            Controls.Add(btnDeleteOrder);
            Controls.Add(btnEditOrder);
            Controls.Add(btnExportPdf);
            Controls.Add(btnViewInvoice);
            Controls.Add(dgvOrders);
            Controls.Add(dtpOrderDate);
            Controls.Add(label1);
            Controls.Add(lblMode);
            Name = "SupplyOrderForm";
            Text = "Phiếu nhập kho";
            Load += SupplyOrderForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPendingLines).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvOrders;
        private DataGridView dgvPendingLines;
        private DateTimePicker dtpOrderDate;
        private CheckBox chkUseExisting;
        private ComboBox cmbExistingItem;
        private TextBox txtItemCode;
        private TextBox txtItemName;
        private TextBox txtItemQty;
        private ComboBox cmbMeasurementUnit;
        private ComboBox cmbCategory;
        private DateTimePicker dtpProductionDate;
        private DateTimePicker dtpExpirationDate;
        private Button btnAddLine;
        private Button btnRemoveLine;
        private Button btnSaveOrder;
        private Button btnNewOrder;
        private Button btnViewInvoice;
        private Button btnExportPdf;
        private Button btnEditOrder;
        private Button btnDeleteOrder;
        private Label lblMode;
        private Label label1;
    }
}
