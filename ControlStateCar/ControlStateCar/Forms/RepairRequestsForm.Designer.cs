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
        private System.Windows.Forms.ComboBox cmbAssignTech;
        private System.Windows.Forms.Button btnAssign;
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
            this.cmbAssignTech = new System.Windows.Forms.ComboBox();
            this.btnAssign = new System.Windows.Forms.Button();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
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
            this.pnlFilter.Controls.Add(this.cmbAssignTech);
            this.pnlFilter.Controls.Add(this.btnAssign);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1020, 65);
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
            this.txtCarNumber.Size = new System.Drawing.Size(100, 20);
            this.txtCarNumber.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(125, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Статус:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(128, 27);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 21);
            this.cmbStatus.TabIndex = 3;
            // 
            // lblTechnician
            // 
            this.lblTechnician.AutoSize = true;
            this.lblTechnician.Location = new System.Drawing.Point(238, 10);
            this.lblTechnician.Name = "lblTechnician";
            this.lblTechnician.Size = new System.Drawing.Size(46, 13);
            this.lblTechnician.TabIndex = 4;
            this.lblTechnician.Text = "Техник:";
            // 
            // txtTechnician
            // 
            this.txtTechnician.Location = new System.Drawing.Point(241, 28);
            this.txtTechnician.Name = "txtTechnician";
            this.txtTechnician.Size = new System.Drawing.Size(100, 20);
            this.txtTechnician.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(351, 10);
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
            this.dtpFilterDate.Location = new System.Drawing.Point(354, 28);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.Size = new System.Drawing.Size(110, 20);
            this.dtpFilterDate.TabIndex = 7;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(475, 24);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 27);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(556, 24);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 27);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cmbAssignTech
            // 
            this.cmbAssignTech.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignTech.FormattingEnabled = true;
            this.cmbAssignTech.Location = new System.Drawing.Point(660, 27);
            this.cmbAssignTech.Name = "cmbAssignTech";
            this.cmbAssignTech.Size = new System.Drawing.Size(140, 21);
            this.cmbAssignTech.TabIndex = 10;
            // 
            // btnAssign
            // 
            this.btnAssign.Location = new System.Drawing.Point(806, 24);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(95, 27);
            this.btnAssign.TabIndex = 11;
            this.btnAssign.Text = "Назначить";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // dgvRequests
            // 
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToDeleteRows = false;
            this.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRequests.Location = new System.Drawing.Point(0, 65);
            this.dgvRequests.MultiSelect = false;
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersVisible = false;
            this.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequests.Size = new System.Drawing.Size(1020, 415);
            this.dgvRequests.TabIndex = 1;
            this.dgvRequests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequests_CellContentClick);
            // 
            // RepairRequestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 480);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.pnlFilter);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "RepairRequestsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список заявок на ремонт";
            this.Load += new System.EventHandler(this.RepairRequestsForm_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.ResumeLayout(false);
        }
    }
}