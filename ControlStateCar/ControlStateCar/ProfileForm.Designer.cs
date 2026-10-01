namespace ControlStateCar
{
    partial class ProfileForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUserInfo;
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
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnRegisterEmployee = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUserInfo.Location = new System.Drawing.Point(25, 25);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(155, 15);
            this.lblUserInfo.TabIndex = 2;
            this.lblUserInfo.Text = "Пользователь: | Роль:";
            // 
            // btnRegisterEmployee
            // 
            this.btnRegisterEmployee.Location = new System.Drawing.Point(28, 65);
            this.btnRegisterEmployee.Name = "btnRegisterEmployee";
            this.btnRegisterEmployee.Size = new System.Drawing.Size(180, 32);
            this.btnRegisterEmployee.TabIndex = 0;
            this.btnRegisterEmployee.Text = "Добавить сотрудника";
            this.btnRegisterEmployee.UseVisualStyleBackColor = true;
            this.btnRegisterEmployee.Click += new System.EventHandler(this.btnRegisterEmployee_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(28, 110);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(180, 32);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Выйти";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 170);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnRegisterEmployee);
            this.Controls.Add(this.lblUserInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Профиль сотрудника";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}