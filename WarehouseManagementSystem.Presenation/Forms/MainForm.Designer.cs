namespace WarehouseManagementSystem.Presenation
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            SidePanal = new Panel();
            panelTopBar = new Panel();
            lblUser = new Label();
            btnLogout = new Button();
            btnActivityLog = new FontAwesome.Sharp.IconButton();
            btnUsers = new FontAwesome.Sharp.IconButton();
            btnDashboard = new FontAwesome.Sharp.IconButton();
            btnItemsCloseToExpiration = new FontAwesome.Sharp.IconButton();
            btnItemsInWarehousePeriodReport = new FontAwesome.Sharp.IconButton();
            btnWarehousRepot = new FontAwesome.Sharp.IconButton();
            btnSupplyOrder = new FontAwesome.Sharp.IconButton();
            btnWithdrawalOrder = new FontAwesome.Sharp.IconButton();
            btnItems = new FontAwesome.Sharp.IconButton();
            btnWarehouse = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            panelContainer = new Panel();
            SidePanal.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // SidePanal
            // 
            SidePanal.BackColor = Color.Black;
            SidePanal.Controls.Add(btnActivityLog);
            SidePanal.Controls.Add(btnUsers);
            SidePanal.Controls.Add(btnDashboard);
            SidePanal.Controls.Add(btnItemsCloseToExpiration);
            SidePanal.Controls.Add(btnItemsInWarehousePeriodReport);
            SidePanal.Controls.Add(btnWarehousRepot);
            SidePanal.Controls.Add(btnSupplyOrder);
            SidePanal.Controls.Add(btnWithdrawalOrder);
            SidePanal.Controls.Add(btnItems);
            SidePanal.Controls.Add(btnWarehouse);
            SidePanal.Controls.Add(panel1);
            SidePanal.Dock = DockStyle.Left;
            SidePanal.Location = new Point(0, 0);
            SidePanal.Name = "SidePanal";
            SidePanal.Size = new Size(220, 885);
            SidePanal.TabIndex = 0;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.WhiteSmoke;
            btnDashboard.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            btnDashboard.IconColor = Color.WhiteSmoke;
            btnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDashboard.Padding = new Padding(10, 0, 20, 0);
            btnDashboard.Size = new Size(220, 60);
            btnDashboard.Text = "Tổng quan";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // btnUsers
            // 
            btnUsers.Dock = DockStyle.Top;
            btnUsers.FlatAppearance.BorderSize = 0;
            btnUsers.FlatStyle = FlatStyle.Flat;
            btnUsers.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnUsers.ForeColor = Color.WhiteSmoke;
            btnUsers.IconChar = FontAwesome.Sharp.IconChar.Users;
            btnUsers.IconColor = Color.WhiteSmoke;
            btnUsers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUsers.ImageAlign = ContentAlignment.MiddleLeft;
            btnUsers.Padding = new Padding(10, 0, 20, 0);
            btnUsers.Size = new Size(220, 60);
            btnUsers.Text = "Người dùng";
            btnUsers.TextAlign = ContentAlignment.MiddleLeft;
            btnUsers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Click += btnUsers_Click;
            // 
            // btnActivityLog
            // 
            btnActivityLog.Dock = DockStyle.Top;
            btnActivityLog.FlatAppearance.BorderSize = 0;
            btnActivityLog.FlatStyle = FlatStyle.Flat;
            btnActivityLog.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnActivityLog.ForeColor = Color.WhiteSmoke;
            btnActivityLog.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            btnActivityLog.IconColor = Color.WhiteSmoke;
            btnActivityLog.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnActivityLog.ImageAlign = ContentAlignment.MiddleLeft;
            btnActivityLog.Padding = new Padding(10, 0, 20, 0);
            btnActivityLog.Size = new Size(220, 60);
            btnActivityLog.Text = "Nhật ký";
            btnActivityLog.TextAlign = ContentAlignment.MiddleLeft;
            btnActivityLog.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnActivityLog.UseVisualStyleBackColor = true;
            btnActivityLog.Click += btnActivityLog_Click;
            // 
            // btnItemsCloseToExpiration
            // 
            btnItemsCloseToExpiration.Dock = DockStyle.Top;
            btnItemsCloseToExpiration.FlatAppearance.BorderSize = 0;
            btnItemsCloseToExpiration.FlatStyle = FlatStyle.Flat;
            btnItemsCloseToExpiration.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnItemsCloseToExpiration.ForeColor = Color.WhiteSmoke;
            btnItemsCloseToExpiration.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            btnItemsCloseToExpiration.IconColor = Color.WhiteSmoke;
            btnItemsCloseToExpiration.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnItemsCloseToExpiration.ImageAlign = ContentAlignment.MiddleLeft;
            btnItemsCloseToExpiration.Location = new Point(0, 579);
            btnItemsCloseToExpiration.Name = "btnItemsCloseToExpiration";
            btnItemsCloseToExpiration.Padding = new Padding(10, 0, 20, 0);
            btnItemsCloseToExpiration.Size = new Size(220, 60);
            btnItemsCloseToExpiration.TabIndex = 11;
            btnItemsCloseToExpiration.Text = "Hàng sắp hết hạn";
            btnItemsCloseToExpiration.TextAlign = ContentAlignment.MiddleLeft;
            btnItemsCloseToExpiration.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnItemsCloseToExpiration.UseVisualStyleBackColor = true;
            btnItemsCloseToExpiration.Click += btnItemsCloseToExpiration_Click;
            // 
            // btnItemsInWarehousePeriodReport
            // 
            btnItemsInWarehousePeriodReport.Dock = DockStyle.Top;
            btnItemsInWarehousePeriodReport.FlatAppearance.BorderSize = 0;
            btnItemsInWarehousePeriodReport.FlatStyle = FlatStyle.Flat;
            btnItemsInWarehousePeriodReport.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnItemsInWarehousePeriodReport.ForeColor = Color.WhiteSmoke;
            btnItemsInWarehousePeriodReport.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            btnItemsInWarehousePeriodReport.IconColor = Color.WhiteSmoke;
            btnItemsInWarehousePeriodReport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnItemsInWarehousePeriodReport.ImageAlign = ContentAlignment.MiddleLeft;
            btnItemsInWarehousePeriodReport.Location = new Point(0, 500);
            btnItemsInWarehousePeriodReport.Name = "btnItemsInWarehousePeriodReport";
            btnItemsInWarehousePeriodReport.Padding = new Padding(10, 0, 20, 0);
            btnItemsInWarehousePeriodReport.Size = new Size(220, 79);
            btnItemsInWarehousePeriodReport.TabIndex = 10;
            btnItemsInWarehousePeriodReport.Text = "Báo cáo hàng theo kỳ";
            btnItemsInWarehousePeriodReport.TextAlign = ContentAlignment.MiddleLeft;
            btnItemsInWarehousePeriodReport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnItemsInWarehousePeriodReport.UseVisualStyleBackColor = true;
            btnItemsInWarehousePeriodReport.Click += btnItemsInWarehousePeriodReport_Click;
            // 
            // btnWarehousRepot
            // 
            btnWarehousRepot.Dock = DockStyle.Top;
            btnWarehousRepot.FlatAppearance.BorderSize = 0;
            btnWarehousRepot.FlatStyle = FlatStyle.Flat;
            btnWarehousRepot.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnWarehousRepot.ForeColor = Color.WhiteSmoke;
            btnWarehousRepot.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            btnWarehousRepot.IconColor = Color.WhiteSmoke;
            btnWarehousRepot.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnWarehousRepot.ImageAlign = ContentAlignment.MiddleLeft;
            btnWarehousRepot.Location = new Point(0, 440);
            btnWarehousRepot.Name = "btnWarehousRepot";
            btnWarehousRepot.Padding = new Padding(10, 0, 20, 0);
            btnWarehousRepot.Size = new Size(220, 60);
            btnWarehousRepot.TabIndex = 8;
            btnWarehousRepot.Text = "Báo cáo tình trạng kho";
            btnWarehousRepot.TextAlign = ContentAlignment.MiddleLeft;
            btnWarehousRepot.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnWarehousRepot.UseVisualStyleBackColor = true;
            btnWarehousRepot.Click += btnWarehousRepot_Click;
            // 
            // btnSupplyOrder
            // 
            btnSupplyOrder.Dock = DockStyle.Top;
            btnSupplyOrder.FlatAppearance.BorderSize = 0;
            btnSupplyOrder.FlatStyle = FlatStyle.Flat;
            btnSupplyOrder.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSupplyOrder.ForeColor = Color.WhiteSmoke;
            btnSupplyOrder.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleRight;
            btnSupplyOrder.IconColor = Color.WhiteSmoke;
            btnSupplyOrder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSupplyOrder.ImageAlign = ContentAlignment.MiddleLeft;
            btnSupplyOrder.Location = new Point(0, 320);
            btnSupplyOrder.Name = "btnSupplyOrder";
            btnSupplyOrder.Padding = new Padding(10, 0, 20, 0);
            btnSupplyOrder.Size = new Size(220, 60);
            btnSupplyOrder.TabIndex = 5;
            btnSupplyOrder.Text = "Phiếu nhập";
            btnSupplyOrder.TextAlign = ContentAlignment.MiddleLeft;
            btnSupplyOrder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSupplyOrder.UseVisualStyleBackColor = true;
            btnSupplyOrder.Click += btnSupplyOrder_Click;
            // 
            // btnWithdrawalOrder
            // 
            btnWithdrawalOrder.Dock = DockStyle.Top;
            btnWithdrawalOrder.FlatAppearance.BorderSize = 0;
            btnWithdrawalOrder.FlatStyle = FlatStyle.Flat;
            btnWithdrawalOrder.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnWithdrawalOrder.ForeColor = Color.WhiteSmoke;
            btnWithdrawalOrder.IconChar = FontAwesome.Sharp.IconChar.ArrowRightFromBracket;
            btnWithdrawalOrder.IconColor = Color.WhiteSmoke;
            btnWithdrawalOrder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnWithdrawalOrder.ImageAlign = ContentAlignment.MiddleLeft;
            btnWithdrawalOrder.Name = "btnWithdrawalOrder";
            btnWithdrawalOrder.Padding = new Padding(10, 0, 20, 0);
            btnWithdrawalOrder.Size = new Size(220, 60);
            btnWithdrawalOrder.Text = "Phiếu xuất";
            btnWithdrawalOrder.TextAlign = ContentAlignment.MiddleLeft;
            btnWithdrawalOrder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnWithdrawalOrder.UseVisualStyleBackColor = true;
            btnWithdrawalOrder.Click += btnWithdrawalOrder_Click;
            // 
            // btnItems
            // 
            btnItems.Dock = DockStyle.Top;
            btnItems.FlatAppearance.BorderSize = 0;
            btnItems.FlatStyle = FlatStyle.Flat;
            btnItems.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnItems.ForeColor = Color.WhiteSmoke;
            btnItems.IconChar = FontAwesome.Sharp.IconChar.BoxesStacked;
            btnItems.IconColor = Color.WhiteSmoke;
            btnItems.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnItems.ImageAlign = ContentAlignment.MiddleLeft;
            btnItems.Location = new Point(0, 200);
            btnItems.Name = "btnItems";
            btnItems.Padding = new Padding(10, 0, 20, 0);
            btnItems.Size = new Size(220, 60);
            btnItems.TabIndex = 2;
            btnItems.Text = "Mặt hàng";
            btnItems.TextAlign = ContentAlignment.MiddleLeft;
            btnItems.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnItems.UseVisualStyleBackColor = true;
            btnItems.Click += btnItems_Click;
            // 
            // btnWarehouse
            // 
            btnWarehouse.Dock = DockStyle.Top;
            btnWarehouse.FlatAppearance.BorderSize = 0;
            btnWarehouse.FlatStyle = FlatStyle.Flat;
            btnWarehouse.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnWarehouse.ForeColor = Color.WhiteSmoke;
            btnWarehouse.IconChar = FontAwesome.Sharp.IconChar.Warehouse;
            btnWarehouse.IconColor = Color.WhiteSmoke;
            btnWarehouse.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnWarehouse.ImageAlign = ContentAlignment.MiddleLeft;
            btnWarehouse.Location = new Point(0, 140);
            btnWarehouse.Name = "btnWarehouse";
            btnWarehouse.Padding = new Padding(10, 0, 20, 0);
            btnWarehouse.Size = new Size(220, 60);
            btnWarehouse.TabIndex = 1;
            btnWarehouse.Text = "Thông tin kho";
            btnWarehouse.TextAlign = ContentAlignment.MiddleLeft;
            btnWarehouse.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnWarehouse.UseVisualStyleBackColor = true;
            btnWarehouse.Click += btnWarehouse_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10, 0, 20, 0);
            panel1.Size = new Size(220, 140);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(13, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(204, 111);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelTopBar
            // 
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Height = 40;
            panelTopBar.Controls.Add(lblUser);
            panelTopBar.Controls.Add(btnLogout);
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUser.Location = new Point(230, 8);
            lblUser.Text = "Người dùng";
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(1300, 5);
            btnLogout.Size = new Size(100, 30);
            btnLogout.Text = "Đăng xuất";
            btnLogout.Click += btnLogout_Click;
            // 
            // panelContainer
            // 
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Location = new Point(220, 40);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(1200, 845);
            panelContainer.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1420, 885);
            Controls.Add(panelContainer);
            Controls.Add(panelTopBar);
            Controls.Add(SidePanal);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Hệ thống quản lý kho";
            WindowState = FormWindowState.Maximized;
            SidePanal.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel SidePanal;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton btnWarehouse;
        private FontAwesome.Sharp.IconButton btnSupplyOrder;
        private FontAwesome.Sharp.IconButton btnWithdrawalOrder;
        private FontAwesome.Sharp.IconButton btnItems;
        private PictureBox pictureBox1;
        private Panel panelContainer;
        private FontAwesome.Sharp.IconButton btnWarehousRepot;
        private FontAwesome.Sharp.IconButton btnItemsInWarehousePeriodReport;
        private FontAwesome.Sharp.IconButton btnItemsCloseToExpiration;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private FontAwesome.Sharp.IconButton btnActivityLog;
        private FontAwesome.Sharp.IconButton btnUsers;
        private Panel panelTopBar;
        private Label lblUser;
        private Button btnLogout;
    }
}