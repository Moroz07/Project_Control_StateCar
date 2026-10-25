namespace ControlStateCar
{
    partial class ProfileForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnRepairRequests;
        private System.Windows.Forms.Button btnRegisterEmployee;
        private System.Windows.Forms.Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileForm));
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnRepairRequests = new System.Windows.Forms.Button();
            this.btnRegisterEmployee = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUserInfo.Location = new System.Drawing.Point(25, 20);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(136, 15);
            this.lblUserInfo.TabIndex = 3;
            this.lblUserInfo.Text = "Пользователь: | Роль:";
            // 
            // btnRepairRequests
            // 
            this.btnRepairRequests.Location = new System.Drawing.Point(28, 55);
            this.btnRepairRequests.Name = "btnRepairRequests";
            this.btnRepairRequests.Size = new System.Drawing.Size(180, 32);
            this.btnRepairRequests.TabIndex = 0;
            this.btnRepairRequests.Text = "Список заявок";
            this.btnRepairRequests.UseVisualStyleBackColor = true;
            this.btnRepairRequests.Click += new System.EventHandler(this.btnRepairRequests_Click);
            // 
            // btnRegisterEmployee
            // 
            this.btnRegisterEmployee.Location = new System.Drawing.Point(28, 98);
            this.btnRegisterEmployee.Name = "btnRegisterEmployee";
            this.btnRegisterEmployee.Size = new System.Drawing.Size(180, 32);
            this.btnRegisterEmployee.TabIndex = 1;
            this.btnRegisterEmployee.Text = "Добавить сотрудника";
            this.btnRegisterEmployee.UseVisualStyleBackColor = true;
            this.btnRegisterEmployee.Click += new System.EventHandler(this.btnRegisterEmployee_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(28, 141);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(180, 32);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Выйти";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 210);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnRegisterEmployee);
            this.Controls.Add(this.btnRepairRequests);
            this.Controls.Add(this.lblUserInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Профиль сотрудника";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}