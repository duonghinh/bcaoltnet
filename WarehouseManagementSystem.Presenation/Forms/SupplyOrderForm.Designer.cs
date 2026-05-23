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
            btnViewInvoice.Size = new Size(90, 30);
            btnViewInvoice.Text = "Xem HĐ";
            btnViewInvoice.Click += btnViewInvoice_Click;
            btnExportPdf.Location = new Point(112, 262);
            btnExportPdf.Size = new Size(130, 30);
            btnExportPdf.Text = "Hóa đơn + PDF";
            btnExportPdf.Click += btnExportPdf_Click;
            btnEditOrder.Location = new Point(252, 262);
            btnEditOrder.Size = new Size(80, 30);
            btnEditOrder.Text = "Sửa";
            btnEditOrder.Click += btnEditOrder_Click;
            btnDeleteOrder.Location = new Point(342, 262);
            btnDeleteOrder.Size = new Size(80, 30);
            btnDeleteOrder.Text = "Xóa";
            btnDeleteOrder.Click += btnDeleteOrder_Click;
            btnNewOrder.Location = new Point(432, 262);
            btnNewOrder.Size = new Size(100, 30);
            btnNewOrder.Text = "Phiếu mới";
            btnNewOrder.Click += btnNewOrder_Click;
            // 
            chkUseExisting.Location = new Point(12, 305);
            chkUseExisting.Text = "Chọn hàng có sẵn";
            chkUseExisting.AutoSize = true;
            chkUseExisting.CheckedChanged += chkUseExisting_CheckedChanged;
            cmbExistingItem.Location = new Point(160, 301);
            cmbExistingItem.Size = new Size(320, 28);
            // 
            txtItemCode.Location = new Point(12, 345);
            txtItemCode.Size = new Size(180, 27);
            txtItemCode.PlaceholderText = "Mã hàng";
            txtItemName.Location = new Point(220, 345);
            txtItemName.Size = new Size(260, 27);
            txtItemName.PlaceholderText = "Tên hàng";
            txtItemQty.Location = new Point(500, 345);
            txtItemQty.Size = new Size(90, 27);
            txtItemQty.PlaceholderText = "Số lượng";
            cmbMeasurementUnit.Location = new Point(610, 345);
            cmbMeasurementUnit.Size = new Size(110, 28);
            cmbCategory.Location = new Point(740, 345);
            cmbCategory.Size = new Size(150, 28);
            dtpProductionDate.Location = new Point(12, 385);
            dtpProductionDate.Size = new Size(220, 27);
            dtpExpirationDate.Location = new Point(250, 385);
            dtpExpirationDate.Size = new Size(220, 27);
            btnAddLine.Location = new Point(500, 383);
            btnAddLine.Size = new Size(110, 30);
            btnAddLine.Text = "Thêm dòng";
            btnAddLine.Click += btnAddLine_Click;
            btnRemoveLine.Location = new Point(620, 383);
            btnRemoveLine.Size = new Size(100, 30);
            btnRemoveLine.Text = "Xóa dòng";
            btnRemoveLine.Click += btnRemoveLine_Click;
            btnSaveOrder.Location = new Point(730, 383);
            btnSaveOrder.Size = new Size(110, 30);
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
