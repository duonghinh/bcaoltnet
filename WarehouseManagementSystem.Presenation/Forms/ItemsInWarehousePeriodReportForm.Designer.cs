namespace WarehouseManagementSystem.Presenation.Forms
{
    partial class ItemsInWarehousePeriodReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new Label();
            btnGenerateReport = new Button();
            dtpFromDate = new DateTimePicker();
            dtpToDate = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = SystemColors.ActiveCaptionText;
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1333, 125);
            panelHeader.TabIndex = 7;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = SystemColors.ButtonFace;
            lblTitle.Location = new Point(389, 39);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(380, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Báo cáo hàng theo kỳ";
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.Location = new Point(609, 327);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(114, 41);
            btnGenerateReport.TabIndex = 2;
            btnGenerateReport.Text = "Tạo báo cáo";
            btnGenerateReport.UseVisualStyleBackColor = true;
            btnGenerateReport.Click += btnGenerateReport_Click;
            // 
            // dtpFromDate
            // 
            dtpFromDate.Location = new Point(163, 188);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(284, 27);
            dtpFromDate.TabIndex = 3;
            // 
            // dtpToDate
            // 
            dtpToDate.Location = new Point(510, 188);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(266, 27);
            dtpToDate.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(510, 150);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 5;
            label2.Text = "Đến ngày";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(163, 150);
            label3.Name = "label3";
            label3.Size = new Size(64, 20);
            label3.TabIndex = 6;
            label3.Text = "Từ ngày";
            // 
            // ItemsInWarehousePeriodReportForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 491);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dtpToDate);
            Controls.Add(dtpFromDate);
            Controls.Add(btnGenerateReport);
            Controls.Add(panelHeader);
            Name = "ItemsInWarehousePeriodReportForm";
            Text = "Báo cáo hàng theo kỳ";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel panelHeader;
        private Label lblTitle;
        private Button btnGenerateReport;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private Label label2;
        private Label label3;
    }
}
