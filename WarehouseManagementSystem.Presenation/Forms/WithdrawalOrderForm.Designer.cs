namespace WarehouseManagementSystem.Presenation.Forms
{
    partial class WithdrawalOrderForm
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
            txtRecipient = new TextBox();
            cmbItem = new ComboBox();
            txtQty = new TextBox();
            lblStockInfo = new Label();
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
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPendingLines).BeginInit();
            SuspendLayout();
            lblMode.AutoSize = true;
            lblMode.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMode.Location = new Point(12, 12);
            lblMode.Text = "Tạo phiếu xuất mới";
            label1.Location = new Point(12, 42);
            label1.Text = "Ngày phiếu:";
            dtpOrderDate.Location = new Point(100, 38);
            dtpOrderDate.Size = new Size(250, 27);
            label2.Location = new Point(380, 42);
            label2.Text = "Người nhận:";
            txtRecipient.Location = new Point(480, 38);
            txtRecipient.Size = new Size(300, 27);
            dgvOrders.Location = new Point(12, 75);
            dgvOrders.Size = new Size(1350, 180);
            dgvOrders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            cmbItem.Location = new Point(12, 305);
            cmbItem.Size = new Size(380, 28);
            cmbItem.SelectedIndexChanged += cmbItem_SelectedIndexChanged;
            lblStockInfo.Location = new Point(410, 308);
            lblStockInfo.Size = new Size(170, 23);
            txtQty.Location = new Point(600, 305);
            txtQty.Size = new Size(90, 27);
            txtQty.PlaceholderText = "Số lượng";
            btnAddLine.Location = new Point(710, 303);
            btnAddLine.Size = new Size(110, 30);
            btnAddLine.Text = "Thêm dòng";
            btnAddLine.Click += btnAddLine_Click;
            btnRemoveLine.Location = new Point(830, 303);
            btnRemoveLine.Size = new Size(100, 30);
            btnRemoveLine.Text = "Xóa dòng";
            btnRemoveLine.Click += btnRemoveLine_Click;
            btnSaveOrder.Location = new Point(940, 303);
            btnSaveOrder.Size = new Size(110, 30);
            btnSaveOrder.Text = "Lưu phiếu";
            btnSaveOrder.BackColor = Color.OrangeRed;
            btnSaveOrder.ForeColor = Color.White;
            btnSaveOrder.Click += btnSaveOrder_Click;
            dgvPendingLines.Location = new Point(12, 345);
            dgvPendingLines.Size = new Size(1350, 200);
            dgvPendingLines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPendingLines.ReadOnly = true;
            dgvPendingLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ClientSize = new Size(1380, 560);
            Controls.Add(dgvPendingLines);
            Controls.Add(btnSaveOrder);
            Controls.Add(btnRemoveLine);
            Controls.Add(btnAddLine);
            Controls.Add(txtQty);
            Controls.Add(lblStockInfo);
            Controls.Add(cmbItem);
            Controls.Add(txtRecipient);
            Controls.Add(btnNewOrder);
            Controls.Add(btnDeleteOrder);
            Controls.Add(btnEditOrder);
            Controls.Add(btnExportPdf);
            Controls.Add(btnViewInvoice);
            Controls.Add(dgvOrders);
            Controls.Add(dtpOrderDate);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblMode);
            Name = "WithdrawalOrderForm";
            Text = "Phiếu xuất kho";
            Load += WithdrawalOrderForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPendingLines).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvOrders;
        private DataGridView dgvPendingLines;
        private DateTimePicker dtpOrderDate;
        private TextBox txtRecipient;
        private ComboBox cmbItem;
        private TextBox txtQty;
        private Label lblStockInfo;
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
        private Label label2;
    }
}
