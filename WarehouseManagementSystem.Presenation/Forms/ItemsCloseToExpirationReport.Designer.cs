namespace WarehouseManagementSystem.Presenation.Forms

{

    partial class ItemsCloseToExpirationReport

    {

        private System.ComponentModel.IContainer components = null;



        protected override void Dispose(bool disposing)

        {

            if (disposing && (components != null))

            {

                components.Dispose();

            }

            base.Dispose(disposing);

        }



        #region Windows Form Designer generated code



        private void InitializeComponent()

        {

            panelHeader = new Panel();

            lblTitle = new Label();

            nudDaysThreshold = new NumericUpDown();

            label1 = new Label();

            btnGenerateReport = new Button();

            panelHeader.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)nudDaysThreshold).BeginInit();

            SuspendLayout();

            // 

            // panelHeader

            // 

            panelHeader.BackColor = SystemColors.ActiveCaptionText;

            panelHeader.Controls.Add(lblTitle);

            panelHeader.Dock = DockStyle.Top;

            panelHeader.Location = new Point(0, 0);

            panelHeader.Name = "panelHeader";

            panelHeader.Size = new Size(1322, 125);

            panelHeader.TabIndex = 3;

            // 

            // lblTitle

            // 

            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            lblTitle.AutoSize = true;

            lblTitle.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);

            lblTitle.ForeColor = SystemColors.ButtonFace;

            lblTitle.Location = new Point(389, 39);

            lblTitle.Name = "lblTitle";

            lblTitle.Size = new Size(520, 46);

            lblTitle.TabIndex = 0;

            lblTitle.Text = "Báo cáo hàng sắp hết hạn";

            // 

            // nudDaysThreshold

            // 

            nudDaysThreshold.Location = new Point(650, 196);

            nudDaysThreshold.Name = "nudDaysThreshold";

            nudDaysThreshold.Size = new Size(150, 27);

            nudDaysThreshold.TabIndex = 0;

            // 

            // label1

            // 

            label1.AutoSize = true;

            label1.Location = new Point(503, 198);

            label1.Name = "label1";

            label1.Size = new Size(130, 20);

            label1.TabIndex = 1;

            label1.Text = "Số ngày cảnh báo";

            // 

            // btnGenerateReport

            // 

            btnGenerateReport.Location = new Point(593, 321);

            btnGenerateReport.Name = "btnGenerateReport";

            btnGenerateReport.Size = new Size(134, 46);

            btnGenerateReport.TabIndex = 2;

            btnGenerateReport.Text = "Tạo báo cáo";

            btnGenerateReport.UseVisualStyleBackColor = true;

            btnGenerateReport.Click += btnGenerateReport_Click;

            // 

            // ItemsCloseToExpirationReport

            // 

            AutoScaleDimensions = new SizeF(8F, 20F);

            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(1322, 525);

            Controls.Add(btnGenerateReport);

            Controls.Add(label1);

            Controls.Add(nudDaysThreshold);

            Controls.Add(panelHeader);

            Name = "ItemsCloseToExpirationReport";

            Text = "Báo cáo hàng sắp hết hạn";

            panelHeader.ResumeLayout(false);

            panelHeader.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)nudDaysThreshold).EndInit();

            ResumeLayout(false);

            PerformLayout();

        }



        #endregion



        private Panel panelHeader;

        private Label lblTitle;

        private NumericUpDown nudDaysThreshold;

        private Label label1;

        private Button btnGenerateReport;

    }

}

