namespace WarehouseManagementSystem.Presenation.Forms
{
    partial class OrderInvoiceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new Label();
            lblOrderNumber = new Label();
            lblOrderType = new Label();
            lblWarehouse = new Label();
            lblPartner = new Label();
            lblOrderDate = new Label();
            lblCreatedAt = new Label();
            lblUpdatedAt = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dgvLines = new DataGridView();
            btnExportPdf = new Button();
            btnExportExcel = new Button();
            btnClose = new Button();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
            SuspendLayout();
            // 
            panelHeader.BackColor = Color.FromArgb(240, 240, 240);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblOrderNumber);
            panelHeader.Controls.Add(lblOrderType);
            panelHeader.Controls.Add(lblWarehouse);
            panelHeader.Controls.Add(lblPartner);
            panelHeader.Controls.Add(label1);
            panelHeader.Controls.Add(lblOrderDate);
            panelHeader.Controls.Add(label2);
            panelHeader.Controls.Add(lblCreatedAt);
            panelHeader.Controls.Add(label3);
            panelHeader.Controls.Add(lblUpdatedAt);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(900, 200);
            panelHeader.TabIndex = 0;
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "HÓA ĐƠN";
            // 
            lblOrderNumber.Location = new Point(20, 55);
            lblOrderNumber.Size = new Size(400, 23);
            lblOrderNumber.Text = "Số phiếu:";
            // 
            lblOrderType.Location = new Point(20, 80);
            lblOrderType.Size = new Size(400, 23);
            // 
            lblWarehouse.Location = new Point(20, 105);
            lblWarehouse.Size = new Size(400, 23);
            // 
            lblPartner.Location = new Point(450, 55);
            lblPartner.Size = new Size(420, 23);
            // 
            label1.Location = new Point(450, 85);
            label1.Text = "Ngày chứng từ:";
            // 
            lblOrderDate.Location = new Point(580, 85);
            lblOrderDate.Size = new Size(280, 23);
            // 
            label2.Location = new Point(450, 110);
            label2.Text = "Tạo lúc:";
            // 
            lblCreatedAt.Location = new Point(580, 110);
            lblCreatedAt.Size = new Size(280, 23);
            // 
            label3.Location = new Point(450, 135);
            label3.Text = "Cập nhật:";
            // 
            lblUpdatedAt.Location = new Point(580, 135);
            lblUpdatedAt.Size = new Size(280, 23);
            // 
            dgvLines.Dock = DockStyle.Fill;
            dgvLines.ReadOnly = true;
            dgvLines.AllowUserToAddRows = false;
            dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            btnExportPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportPdf.Location = new Point(480, 520);
            btnExportPdf.Size = new Size(120, 36);
            btnExportPdf.Text = "Xuất PDF";
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            btnExportExcel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportExcel.Location = new Point(610, 520);
            btnExportExcel.Size = new Size(120, 36);
            btnExportExcel.Text = "Xuất Excel";
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(740, 520);
            btnClose.Size = new Size(120, 36);
            btnClose.Text = "Đóng";
            btnClose.Click += btnClose_Click;
            // 
            ClientSize = new Size(900, 570);
            Controls.Add(dgvLines);
            Controls.Add(panelHeader);
            Controls.Add(btnExportPdf);
            Controls.Add(btnExportExcel);
            Controls.Add(btnClose);
            StartPosition = FormStartPosition.CenterParent;
            Name = "OrderInvoiceForm";
            Text = "Hóa đơn";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
            ResumeLayout(false);
        }

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblOrderNumber;
        private Label lblOrderType;
        private Label lblWarehouse;
        private Label lblPartner;
        private Label label1;
        private Label lblOrderDate;
        private Label label2;
        private Label lblCreatedAt;
        private Label label3;
        private Label lblUpdatedAt;
        private DataGridView dgvLines;
        private Button btnExportPdf;
        private Button btnExportExcel;
        private Button btnClose;
    }
}
