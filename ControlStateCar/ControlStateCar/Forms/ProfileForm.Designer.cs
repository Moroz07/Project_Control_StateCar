namespace ControlStateCar
{
    partial class ProfileForm
    {
        private System.ComponentModel.IContainer components = null;

        // Левая боковая панель навигации (графитовая)
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Panel pnlBrandAccent;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblBrandSub;
        private System.Windows.Forms.Button btnRepairRequests;
        private System.Windows.Forms.Button btnRegisterEmployee;
        private System.Windows.Forms.Button btnLogout;

        // Правая рабочая область (Material карточки)
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlStatusBanner;
        private System.Windows.Forms.Panel pnlStatusAccent;
        private System.Windows.Forms.Label lblStatusHeader;
        private System.Windows.Forms.Label lblStatusSub;

        private System.Windows.Forms.Panel pnlUserCard;
        private System.Windows.Forms.Panel pnlUserCardTop;
        private System.Windows.Forms.Label lblCardUserTitle;
        private System.Windows.Forms.Label lblUserInfo;

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

            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.pnlBrandAccent = new System.Windows.Forms.Panel();
            this.lblBrandTitle = new System.Windows.Forms.Label();
            this.lblBrandSub = new System.Windows.Forms.Label();
            this.btnRepairRequests = new System.Windows.Forms.Button();
            this.btnRegisterEmployee = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();

            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlStatusBanner = new System.Windows.Forms.Panel();
            this.pnlStatusAccent = new System.Windows.Forms.Panel();
            this.lblStatusHeader = new System.Windows.Forms.Label();
            this.lblStatusSub = new System.Windows.Forms.Label();

            this.pnlUserCard = new System.Windows.Forms.Panel();
            this.pnlUserCardTop = new System.Windows.Forms.Panel();
            this.lblCardUserTitle = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();

            this.pnlSidebar.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlStatusBanner.SuspendLayout();
            this.pnlUserCard.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlSidebar (Левая панель навигации — Deep Slate)
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(24, 28, 38);
            this.pnlSidebar.Controls.Add(this.btnRegisterEmployee);
            this.pnlSidebar.Controls.Add(this.btnRepairRequests);
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.pnlBrand);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(244, 350);
            this.pnlSidebar.TabIndex = 0;

            // 
            // pnlBrand (Шапка меню)
            // 
            this.pnlBrand.BackColor = System.Drawing.Color.FromArgb(17, 20, 28);
            this.pnlBrand.Controls.Add(this.lblBrandSub);
            this.pnlBrand.Controls.Add(this.lblBrandTitle);
            this.pnlBrand.Controls.Add(this.pnlBrandAccent);
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBrand.Location = new System.Drawing.Point(0, 0);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Size = new System.Drawing.Size(244, 88);
            this.pnlBrand.TabIndex = 0;

            this.pnlBrandAccent.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.pnlBrandAccent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBrandAccent.Location = new System.Drawing.Point(0, 85);
            this.pnlBrandAccent.Name = "pnlBrandAccent";
            this.pnlBrandAccent.Size = new System.Drawing.Size(244, 3);

            this.lblBrandTitle.AutoSize = true;
            this.lblBrandTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblBrandTitle.ForeColor = System.Drawing.Color.White;
            this.lblBrandTitle.Location = new System.Drawing.Point(20, 18);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(150, 25);
            this.lblBrandTitle.Text = "ControlStateCar";

            this.lblBrandSub.AutoSize = true;
            this.lblBrandSub.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(96, 165, 250);
            this.lblBrandSub.Location = new System.Drawing.Point(22, 47);
            this.lblBrandSub.Name = "lblBrandSub";
            this.lblBrandSub.Size = new System.Drawing.Size(148, 13);
            this.lblBrandSub.Text = "СЕРВИС И ОБСЛУЖИВАНИЕ";

            // 
            // btnRepairRequests (Главная плашка — Cobalt Blue)
            // 
            this.btnRepairRequests.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnRepairRequests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepairRequests.FlatAppearance.BorderSize = 0;
            this.btnRepairRequests.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnRepairRequests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepairRequests.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRepairRequests.ForeColor = System.Drawing.Color.White;
            this.btnRepairRequests.Location = new System.Drawing.Point(0, 110);
            this.btnRepairRequests.Name = "btnRepairRequests";
            this.btnRepairRequests.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnRepairRequests.Size = new System.Drawing.Size(244, 48);
            this.btnRepairRequests.TabIndex = 0;
            this.btnRepairRequests.Text = "Список заявок";
            this.btnRepairRequests.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepairRequests.UseVisualStyleBackColor = false;
            this.btnRepairRequests.Click += new System.EventHandler(this.btnRepairRequests_Click);

            // 
            // btnRegisterEmployee (Плашка добавления сотрудника — Soft Slate)
            // 
            this.btnRegisterEmployee.BackColor = System.Drawing.Color.FromArgb(35, 41, 56);
            this.btnRegisterEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegisterEmployee.FlatAppearance.BorderSize = 0;
            this.btnRegisterEmployee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(48, 56, 76);
            this.btnRegisterEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterEmployee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRegisterEmployee.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnRegisterEmployee.Location = new System.Drawing.Point(0, 166);
            this.btnRegisterEmployee.Name = "btnRegisterEmployee";
            this.btnRegisterEmployee.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnRegisterEmployee.Size = new System.Drawing.Size(244, 48);
            this.btnRegisterEmployee.TabIndex = 1;
            this.btnRegisterEmployee.Text = "Добавить сотрудника";
            this.btnRegisterEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegisterEmployee.UseVisualStyleBackColor = false;
            this.btnRegisterEmployee.Click += new System.EventHandler(this.btnRegisterEmployee_Click);

            // 
            // btnLogout (Нижняя плашка выхода)
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(17, 20, 28);
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnLogout.Location = new System.Drawing.Point(0, 302);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(244, 48);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Выйти из системы";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // 
            // pnlContent (Правая рабочая область — Cool Alabaster)
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlContent.Controls.Add(this.pnlUserCard);
            this.pnlContent.Controls.Add(this.pnlStatusBanner);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(244, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(570, 350);
            this.pnlContent.TabIndex = 1;

            // 
            // pnlStatusBanner (Верхняя карточка состояния системы)
            // 
            this.pnlStatusBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatusBanner.BackColor = System.Drawing.Color.White;
            this.pnlStatusBanner.Controls.Add(this.lblStatusSub);
            this.pnlStatusBanner.Controls.Add(this.lblStatusHeader);
            this.pnlStatusBanner.Controls.Add(this.pnlStatusAccent);
            this.pnlStatusBanner.Location = new System.Drawing.Point(26, 24);
            this.pnlStatusBanner.Name = "pnlStatusBanner";
            this.pnlStatusBanner.Size = new System.Drawing.Size(518, 92);
            this.pnlStatusBanner.TabIndex = 0;

            this.pnlStatusAccent.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.pnlStatusAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlStatusAccent.Location = new System.Drawing.Point(0, 0);
            this.pnlStatusAccent.Name = "pnlStatusAccent";
            this.pnlStatusAccent.Size = new System.Drawing.Size(6, 92);

            this.lblStatusHeader.AutoSize = true;
            this.lblStatusHeader.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusHeader.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblStatusHeader.Location = new System.Drawing.Point(24, 18);
            this.lblStatusHeader.Name = "lblStatusHeader";
            this.lblStatusHeader.Size = new System.Drawing.Size(250, 25);
            this.lblStatusHeader.Text = "Рабочее место сотрудника";

            this.lblStatusSub.AutoSize = true;
            this.lblStatusSub.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStatusSub.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblStatusSub.Location = new System.Drawing.Point(26, 50);
            this.lblStatusSub.Name = "lblStatusSub";
            this.lblStatusSub.Size = new System.Drawing.Size(355, 17);
            this.lblStatusSub.Text = "Система активна  •  Подключение к базе данных установлено";

            // 
            // pnlUserCard (Карточка профиля пользователя с верхним акцентом Indigo)
            // 
            this.pnlUserCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUserCard.BackColor = System.Drawing.Color.White;
            this.pnlUserCard.Controls.Add(this.lblUserInfo);
            this.pnlUserCard.Controls.Add(this.lblCardUserTitle);
            this.pnlUserCard.Controls.Add(this.pnlUserCardTop);
            this.pnlUserCard.Location = new System.Drawing.Point(26, 136);
            this.pnlUserCard.Name = "pnlUserCard";
            this.pnlUserCard.Size = new System.Drawing.Size(518, 92);
            this.pnlUserCard.TabIndex = 1;

            this.pnlUserCardTop.BackColor = System.Drawing.Color.FromArgb(99, 102, 241);
            this.pnlUserCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUserCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlUserCardTop.Name = "pnlUserCardTop";
            this.pnlUserCardTop.Size = new System.Drawing.Size(518, 3);

            this.lblCardUserTitle.AutoSize = true;
            this.lblCardUserTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardUserTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCardUserTitle.Location = new System.Drawing.Point(22, 18);
            this.lblCardUserTitle.Name = "lblCardUserTitle";
            this.lblCardUserTitle.Size = new System.Drawing.Size(164, 13);
            this.lblCardUserTitle.Text = "УЧЁТНАЯ ЗАПИСЬ СЕССИИ";

            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblUserInfo.Location = new System.Drawing.Point(22, 46);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(168, 20);
            this.lblUserInfo.Text = "Пользователь:  |  Роль:";

            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(814, 350);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(760, 350);
            this.Name = "ProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ControlStateCar — Профиль сотрудника";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlBrand.ResumeLayout(false);
            this.pnlBrand.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlStatusBanner.ResumeLayout(false);
            this.pnlStatusBanner.PerformLayout();
            this.pnlUserCard.ResumeLayout(false);
            this.pnlUserCard.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}