namespace WarehouseManagementSystem.Presenation.Forms
{
    partial class ItemsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvItems = new DataGridView();
            txtName = new TextBox();
            txtCode = new TextBox();
            label1 = new Label();
            label2 = new Label();
            cmbUnit = new ComboBox();
            label3 = new Label();
            cmbCategory = new ComboBox();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            label6 = new Label();
            txtQuantity = new TextBox();
            txtSearch = new TextBox();
            cmbFilterCategory = new ComboBox();
            btnSearch = new Button();
            btnExportExcel = new Button();
            label4 = new Label();
            label5 = new Label();
            dtpProduction = new DateTimePicker();
            dtpExpiration = new DateTimePicker();
            label7 = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            txtSearch.Location = new Point(12, 12);
            txtSearch.Size = new Size(280, 27);
            cmbFilterCategory.Location = new Point(310, 12);
            cmbFilterCategory.Size = new Size(150, 28);
            btnSearch.Location = new Point(480, 10);
            btnSearch.Size = new Size(75, 30);
            btnSearch.Text = "Tìm";
            btnSearch.Click += btnSearch_Click;
            btnExportExcel.Location = new Point(565, 10);
            btnExportExcel.Size = new Size(110, 30);
            btnExportExcel.Text = "Xuất Excel";
            btnExportExcel.Click += btnExportExcel_Click;
            label4.Location = new Point(12, 50);
            label4.Text = "Tên hàng";
            txtName.Location = new Point(90, 47);
            txtName.Size = new Size(220, 27);
            label2.Location = new Point(330, 50);
            label2.Text = "Mã hàng";
            txtCode.Location = new Point(400, 47);
            txtCode.Size = new Size(150, 27);
            label3.Location = new Point(575, 50);
            label3.Text = "Đơn vị";
            cmbUnit.Location = new Point(630, 47);
            cmbUnit.Size = new Size(100, 28);
            label5.Location = new Point(750, 50);
            label5.Text = "Nhóm";
            cmbCategory.Location = new Point(810, 47);
            cmbCategory.Size = new Size(145, 28);
            label6.Location = new Point(12, 90);
            label6.Text = "SL tồn";
            txtQuantity.Location = new Point(90, 87);
            txtQuantity.Size = new Size(80, 27);
            label7.Location = new Point(200, 90);
            label7.Text = "Ngày SX";
            dtpProduction.Location = new Point(280, 87);
            dtpProduction.Size = new Size(200, 27);
            label8.Location = new Point(500, 90);
            label8.Text = "Hạn SD";
            dtpExpiration.Location = new Point(580, 87);
            dtpExpiration.Size = new Size(200, 27);
            btnAdd.Location = new Point(985, 45);
            btnAdd.Size = new Size(95, 30);
            btnAdd.Text = "Thêm";
            btnAdd.BackColor = Color.Lime;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Location = new Point(985, 85);
            btnUpdate.Size = new Size(95, 30);
            btnUpdate.Text = "Cập nhật";
            btnUpdate.BackColor = Color.DodgerBlue;
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Location = new Point(1095, 85);
            btnDelete.Size = new Size(95, 30);
            btnDelete.Text = "Xóa";
            btnDelete.BackColor = Color.Red;
            btnDelete.ForeColor = Color.White;
            btnDelete.Click += btnDelete_Click;
            dgvItems.Location = new Point(12, 130);
            dgvItems.Size = new Size(1200, 400);
            dgvItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvItems.Click += dgvItems_Click;
            ClientSize = new Size(1224, 550);
            Controls.Add(dgvItems);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dtpExpiration);
            Controls.Add(label8);
            Controls.Add(dtpProduction);
            Controls.Add(label7);
            Controls.Add(txtQuantity);
            Controls.Add(label6);
            Controls.Add(cmbCategory);
            Controls.Add(label5);
            Controls.Add(cmbUnit);
            Controls.Add(label3);
            Controls.Add(txtCode);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label4);
            Controls.Add(btnExportExcel);
            Controls.Add(btnSearch);
            Controls.Add(cmbFilterCategory);
            Controls.Add(txtSearch);
            Name = "ItemsForm";
            Text = "Quản lý mặt hàng";
            Load += ItemsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvItems;
        private TextBox txtName;
        private TextBox txtCode;
        private Label label1;
        private Label label2;
        private ComboBox cmbUnit;
        private Label label3;
        private ComboBox cmbCategory;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnAdd;
        private Label label6;
        private TextBox txtQuantity;
        private TextBox txtSearch;
        private ComboBox cmbFilterCategory;
        private Button btnSearch;
        private Button btnExportExcel;
        private Label label4;
        private Label label5;
        private DateTimePicker dtpProduction;
        private DateTimePicker dtpExpiration;
        private Label label7;
        private Label label8;
    }
}
