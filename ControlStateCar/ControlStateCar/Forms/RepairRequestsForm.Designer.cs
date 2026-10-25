namespace ControlStateCar
{
    partial class RepairRequestsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblCarNumber;
        private System.Windows.Forms.TextBox txtCarNumber;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblTechnician;
        private System.Windows.Forms.TextBox txtTechnician;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnReset;

        private System.Windows.Forms.Label lblDivider;
        private System.Windows.Forms.ComboBox cmbAssignTech;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnCancelRequest;

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTableTitle;

        private System.Windows.Forms.DataGridView dgvRequests;

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
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblCarNumber = new System.Windows.Forms.Label();
            this.txtCarNumber = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblTechnician = new System.Windows.Forms.Label();
            this.txtTechnician = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();

            this.lblDivider = new System.Windows.Forms.Label();
            this.cmbAssignTech = new System.Windows.Forms.ComboBox();
            this.btnAssign = new System.Windows.Forms.Button();
            this.btnCancelRequest = new System.Windows.Forms.Button();

            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTableTitle = new System.Windows.Forms.Label();

            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.pnlFilter.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.btnCancelRequest);
            this.pnlFilter.Controls.Add(this.btnAssign);
            this.pnlFilter.Controls.Add(this.cmbAssignTech);
            this.pnlFilter.Controls.Add(this.lblDivider);
            this.pnlFilter.Controls.Add(this.btnReset);
            this.pnlFilter.Controls.Add(this.btnApply);
            this.pnlFilter.Controls.Add(this.dtpFilterDate);
            this.pnlFilter.Controls.Add(this.lblDate);
            this.pnlFilter.Controls.Add(this.txtTechnician);
            this.pnlFilter.Controls.Add(this.lblTechnician);
            this.pnlFilter.Controls.Add(this.cmbStatus);
            this.pnlFilter.Controls.Add(this.lblStatus);
            this.pnlFilter.Controls.Add(this.txtCarNumber);
            this.pnlFilter.Controls.Add(this.lblCarNumber);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1120, 65);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblCarNumber
            // 
            this.lblCarNumber.AutoSize = true;
            this.lblCarNumber.Location = new System.Drawing.Point(12, 10);
            this.lblCarNumber.Name = "lblCarNumber";
            this.lblCarNumber.Size = new System.Drawing.Size(58, 13);
            this.lblCarNumber.TabIndex = 0;
            this.lblCarNumber.Text = "Госномер:";
            // 
            // txtCarNumber
            // 
            this.txtCarNumber.Location = new System.Drawing.Point(15, 28);
            this.txtCarNumber.Name = "txtCarNumber";
            this.txtCarNumber.Size = new System.Drawing.Size(80, 20);
            this.txtCarNumber.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(103, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Статус:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(106, 27);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(95, 21);
            this.cmbStatus.TabIndex = 3;
            // 
            // lblTechnician
            // 
            this.lblTechnician.AutoSize = true;
            this.lblTechnician.Location = new System.Drawing.Point(208, 10);
            this.lblTechnician.Name = "lblTechnician";
            this.lblTechnician.Size = new System.Drawing.Size(46, 13);
            this.lblTechnician.TabIndex = 4;
            this.lblTechnician.Text = "Техник:";
            // 
            // txtTechnician
            // 
            this.txtTechnician.Location = new System.Drawing.Point(211, 28);
            this.txtTechnician.Name = "txtTechnician";
            this.txtTechnician.Size = new System.Drawing.Size(85, 20);
            this.txtTechnician.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(304, 10);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(36, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Дата:";
            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilterDate.ShowCheckBox = true;
            this.dtpFilterDate.Checked = false;
            this.dtpFilterDate.Location = new System.Drawing.Point(307, 28);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.Size = new System.Drawing.Size(100, 20);
            this.dtpFilterDate.TabIndex = 7;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(415, 24);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 26);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(495, 24);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(65, 26);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblDivider
            // 
            this.lblDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDivider.Location = new System.Drawing.Point(575, 10);
            this.lblDivider.Name = "lblDivider";
            this.lblDivider.Size = new System.Drawing.Size(2, 45);
            this.lblDivider.TabIndex = 10;
            // 
            // cmbAssignTech
            // 
            this.cmbAssignTech.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignTech.FormattingEnabled = true;
            this.cmbAssignTech.Location = new System.Drawing.Point(600, 27);
            this.cmbAssignTech.Name = "cmbAssignTech";
            this.cmbAssignTech.Size = new System.Drawing.Size(140, 21);
            this.cmbAssignTech.TabIndex = 11;
            // 
            // btnAssign
            // 
            this.btnAssign.Location = new System.Drawing.Point(746, 24);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(85, 27);
            this.btnAssign.TabIndex = 12;
            this.btnAssign.Text = "Назначить";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // btnCancelRequest
            // 
            this.btnCancelRequest.Location = new System.Drawing.Point(845, 24);
            this.btnCancelRequest.Name = "btnCancelRequest";
            this.btnCancelRequest.Size = new System.Drawing.Size(120, 27);
            this.btnCancelRequest.TabIndex = 13;
            this.btnCancelRequest.Text = "Отменить заявку";
            this.btnCancelRequest.UseVisualStyleBackColor = true;
            this.btnCancelRequest.Click += new System.EventHandler(this.btnCancelRequest_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTableTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 65);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1120, 32);
            this.pnlTitle.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTableTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(420, 16);
            this.lblTableTitle.TabIndex = 0;
            this.lblTableTitle.Text = "Заявки на ремонт и техническое обслуживание";
            // 
            // dgvRequests
            // 
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToDeleteRows = false;
            this.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRequests.Location = new System.Drawing.Point(0, 97);
            this.dgvRequests.MultiSelect = false;
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersVisible = false;
            this.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequests.Size = new System.Drawing.Size(1120, 383);
            this.dgvRequests.TabIndex = 2;
            this.dgvRequests.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequests_CellDoubleClick);
            // 
            // RepairRequestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 480);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlFilter);
            this.MinimumSize = new System.Drawing.Size(950, 400);
            this.Name = "RepairRequestsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список заявок на ремонт";
            this.Load += new System.EventHandler(this.RepairRequestsForm_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.ResumeLayout(false);

        }
    }
}