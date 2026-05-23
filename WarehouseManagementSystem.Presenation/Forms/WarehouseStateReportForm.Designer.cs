namespace WarehouseManagementSystem.Presenation.Forms
{
    partial class WarehouseStateReportForm
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
            btnGenerateReport = new Button();
            panel1 = new Panel();
            label2 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.Location = new Point(389, 200);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(203, 38);
            btnGenerateReport.TabIndex = 2;
            btnGenerateReport.Text = "Tạo báo cáo";
            btnGenerateReport.UseVisualStyleBackColor = true;
            btnGenerateReport.Click += btnGenerateReport_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1385, 125);
            panel1.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(389, 39);
            label2.Name = "label2";
            label2.Size = new Size(420, 46);
            label2.TabIndex = 0;
            label2.Text = "Báo cáo tình trạng kho";
            // 
            // WarehouseStateReportForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1385, 640);
            Controls.Add(panel1);
            Controls.Add(btnGenerateReport);
            Name = "WarehouseStateReportForm";
            Text = "Báo cáo tình trạng kho";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        private Button btnGenerateReport;
        private Panel panel1;
        private Label label2;
    }
}
