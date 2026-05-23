namespace WarehouseManagementSystem.Presenation.Forms
{
    partial class WarehouseForm
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
            lblTitle = new Label();
            txtName = new TextBox();
            txtManager = new TextBox();
            txtAddress = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnSave = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(24, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(220, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Thông tin kho";
            // 
            // txtName
            // 
            txtName.Location = new Point(24, 110);
            txtName.Name = "txtName";
            txtName.Size = new Size(520, 27);
            txtName.TabIndex = 1;
            // 
            // txtManager
            // 
            txtManager.Location = new Point(24, 250);
            txtManager.Name = "txtManager";
            txtManager.Size = new Size(520, 27);
            txtManager.TabIndex = 3;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(24, 180);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(520, 27);
            txtAddress.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 87);
            label1.Name = "label1";
            label1.Size = new Size(68, 20);
            label1.TabIndex = 4;
            label1.Text = "Tên kho";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 157);
            label2.Name = "label2";
            label2.Size = new Size(58, 20);
            label2.TabIndex = 5;
            label2.Text = "Địa chỉ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 227);
            label3.Name = "label3";
            label3.Size = new Size(109, 20);
            label3.TabIndex = 6;
            label3.Text = "Người quản lý";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(24, 310);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 40);
            btnSave.TabIndex = 4;
            btnSave.Text = "Lưu thông tin";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // WarehouseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(btnSave);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtAddress);
            Controls.Add(txtManager);
            Controls.Add(txtName);
            Controls.Add(lblTitle);
            Name = "WarehouseForm";
            Text = "Thông tin kho";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtName;
        private TextBox txtManager;
        private TextBox txtAddress;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnSave;
    }
}
