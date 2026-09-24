namespace ControlStateCar
{
    partial class EmployeeEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;

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
            this.components = new System.ComponentModel.Container();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(20, 15);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(41, 13);
            this.lblLogin.TabIndex = 8;
            this.lblLogin.Text = "Логин:";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(23, 31);
            this.txtLogin.MaxLength = 50;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(260, 20);
            this.txtLogin.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 60);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(48, 13);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "Пароль:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(23, 76);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(260, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(20, 105);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(37, 13);
            this.lblFullName.TabIndex = 10;
            this.lblFullName.Text = "ФИО:";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(23, 121);
            this.txtFullName.MaxLength = 150;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(260, 20);
            this.txtFullName.TabIndex = 2;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(20, 150);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(35, 13);
            this.lblRole.TabIndex = 11;
            this.lblRole.Text = "Роль:";
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(23, 166);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(260, 21);
            this.cmbRole.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 195);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 12;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(23, 211);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(260, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 240);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(155, 13);
            this.lblPhone.TabIndex = 13;
            this.lblPhone.Text = "Телефон (+7 (XXX) XXX-XX-XX):";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(23, 256);
            this.txtPhone.MaxLength = 20;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(260, 20);
            this.txtPhone.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(23, 300);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(163, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // EmployeeEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(315, 350);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EmployeeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Регистрация сотрудника";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}