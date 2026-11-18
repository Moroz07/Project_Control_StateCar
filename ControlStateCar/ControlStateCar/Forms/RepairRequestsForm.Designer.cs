namespace ControlStateCar
{
    partial class RepairRequestsForm
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

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRibbon = new System.Windows.Forms.Panel();
            this.lblGroupActions = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.sep3 = new System.Windows.Forms.Panel();
            this.lblGroupTech = new System.Windows.Forms.Label();
            this.txtTechnician = new System.Windows.Forms.TextBox();
            this.lblTechnician = new System.Windows.Forms.Label();
            this.sep2 = new System.Windows.Forms.Panel();
            this.lblGroupParams = new System.Windows.Forms.Label();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.sep1 = new System.Windows.Forms.Panel();
            this.lblGroupCar = new System.Windows.Forms.Label();
            this.txtCarNumber = new System.Windows.Forms.TextBox();
            this.lblCarNumber = new System.Windows.Forms.Label();
            this.pnlRibbonBottomBorder = new System.Windows.Forms.Panel();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.pnlBottomActions = new System.Windows.Forms.Panel();
            this.btnCancelRequest = new System.Windows.Forms.Button();
            this.btnAssign = new System.Windows.Forms.Button();
            this.cmbAssignTech = new System.Windows.Forms.ComboBox();
            this.lblAssignTitle = new System.Windows.Forms.Label();
            this.pnlRibbon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.pnlBottomActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRibbon
            // 
            this.pnlRibbon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlRibbon.Controls.Add(this.lblGroupActions);
            this.pnlRibbon.Controls.Add(this.btnReset);
            this.pnlRibbon.Controls.Add(this.btnApply);
            this.pnlRibbon.Controls.Add(this.sep3);
            this.pnlRibbon.Controls.Add(this.lblGroupTech);
            this.pnlRibbon.Controls.Add(this.txtTechnician);
            this.pnlRibbon.Controls.Add(this.lblTechnician);
            this.pnlRibbon.Controls.Add(this.sep2);
            this.pnlRibbon.Controls.Add(this.lblGroupParams);
            this.pnlRibbon.Controls.Add(this.dtpFilterDate);
            this.pnlRibbon.Controls.Add(this.lblDate);
            this.pnlRibbon.Controls.Add(this.cmbStatus);
            this.pnlRibbon.Controls.Add(this.lblStatus);
            this.pnlRibbon.Controls.Add(this.sep1);
            this.pnlRibbon.Controls.Add(this.lblGroupCar);
            this.pnlRibbon.Controls.Add(this.txtCarNumber);
            this.pnlRibbon.Controls.Add(this.lblCarNumber);
            this.pnlRibbon.Controls.Add(this.pnlRibbonBottomBorder);
            this.pnlRibbon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRibbon.Location = new System.Drawing.Point(0, 0);
            this.pnlRibbon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlRibbon.Name = "pnlRibbon";
            this.pnlRibbon.Size = new System.Drawing.Size(1360, 126);
            this.pnlRibbon.TabIndex = 0;
            // 
            // lblGroupActions
            // 
            this.lblGroupActions.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblGroupActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(134)))), ((int)(((byte)(139)))));
            this.lblGroupActions.Location = new System.Drawing.Point(952, 94);
            this.lblGroupActions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupActions.Name = "lblGroupActions";
            this.lblGroupActions.Size = new System.Drawing.Size(335, 20);
            this.lblGroupActions.TabIndex = 0;
            this.lblGroupActions.Text = "Фильтрация";
            this.lblGroupActions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(204)))), ((int)(((byte)(208)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            this.btnReset.Location = new System.Drawing.Point(1133, 27);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(153, 52);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(952, 27);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(167, 52);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Найти";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // sep3
            // 
            this.sep3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(220)))), ((int)(((byte)(224)))));
            this.sep3.Location = new System.Drawing.Point(928, 12);
            this.sep3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sep3.Name = "sep3";
            this.sep3.Size = new System.Drawing.Size(1, 98);
            this.sep3.TabIndex = 6;
            // 
            // lblGroupTech
            // 
            this.lblGroupTech.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblGroupTech.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(134)))), ((int)(((byte)(139)))));
            this.lblGroupTech.Location = new System.Drawing.Point(701, 94);
            this.lblGroupTech.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupTech.Name = "lblGroupTech";
            this.lblGroupTech.Size = new System.Drawing.Size(207, 20);
            this.lblGroupTech.TabIndex = 7;
            this.lblGroupTech.Text = "Исполнитель";
            this.lblGroupTech.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTechnician
            // 
            this.txtTechnician.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTechnician.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTechnician.Location = new System.Drawing.Point(701, 42);
            this.txtTechnician.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTechnician.Name = "txtTechnician";
            this.txtTechnician.Size = new System.Drawing.Size(206, 29);
            this.txtTechnician.TabIndex = 3;
            // 
            // lblTechnician
            // 
            this.lblTechnician.AutoSize = true;
            this.lblTechnician.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTechnician.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            this.lblTechnician.Location = new System.Drawing.Point(701, 17);
            this.lblTechnician.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTechnician.Name = "lblTechnician";
            this.lblTechnician.Size = new System.Drawing.Size(113, 20);
            this.lblTechnician.TabIndex = 8;
            this.lblTechnician.Text = "Логин техника:";
            // 
            // sep2
            // 
            this.sep2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(220)))), ((int)(((byte)(224)))));
            this.sep2.Location = new System.Drawing.Point(680, 12);
            this.sep2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(1, 98);
            this.sep2.TabIndex = 9;
            // 
            // lblGroupParams
            // 
            this.lblGroupParams.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblGroupParams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(134)))), ((int)(((byte)(139)))));
            this.lblGroupParams.Location = new System.Drawing.Point(264, 94);
            this.lblGroupParams.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupParams.Name = "lblGroupParams";
            this.lblGroupParams.Size = new System.Drawing.Size(396, 20);
            this.lblGroupParams.TabIndex = 10;
            this.lblGroupParams.Text = "Параметры заявки";
            this.lblGroupParams.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.Checked = false;
            this.dtpFilterDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilterDate.Location = new System.Drawing.Point(473, 42);
            this.dtpFilterDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.ShowCheckBox = true;
            this.dtpFilterDate.Size = new System.Drawing.Size(185, 29);
            this.dtpFilterDate.TabIndex = 2;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            this.lblDate.Location = new System.Drawing.Point(473, 17);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(113, 20);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "Дата создания:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(264, 42);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(192, 29);
            this.cmbStatus.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            this.lblStatus.Location = new System.Drawing.Point(264, 17);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 20);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Статус:";
            // 
            // sep1
            // 
            this.sep1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(220)))), ((int)(((byte)(224)))));
            this.sep1.Location = new System.Drawing.Point(243, 12);
            this.sep1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(1, 98);
            this.sep1.TabIndex = 13;
            // 
            // lblGroupCar
            // 
            this.lblGroupCar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblGroupCar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(134)))), ((int)(((byte)(139)))));
            this.lblGroupCar.Location = new System.Drawing.Point(24, 94);
            this.lblGroupCar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupCar.Name = "lblGroupCar";
            this.lblGroupCar.Size = new System.Drawing.Size(200, 20);
            this.lblGroupCar.TabIndex = 14;
            this.lblGroupCar.Text = "Автомобиль";
            this.lblGroupCar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCarNumber
            // 
            this.txtCarNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCarNumber.Location = new System.Drawing.Point(24, 42);
            this.txtCarNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCarNumber.Name = "txtCarNumber";
            this.txtCarNumber.Size = new System.Drawing.Size(199, 29);
            this.txtCarNumber.TabIndex = 0;
            // 
            // lblCarNumber
            // 
            this.lblCarNumber.AutoSize = true;
            this.lblCarNumber.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCarNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            this.lblCarNumber.Location = new System.Drawing.Point(24, 17);
            this.lblCarNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCarNumber.Name = "lblCarNumber";
            this.lblCarNumber.Size = new System.Drawing.Size(81, 20);
            this.lblCarNumber.TabIndex = 15;
            this.lblCarNumber.Text = "Госномер:";
            // 
            // pnlRibbonBottomBorder
            // 
            this.pnlRibbonBottomBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(220)))), ((int)(((byte)(224)))));
            this.pnlRibbonBottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRibbonBottomBorder.Location = new System.Drawing.Point(0, 124);
            this.pnlRibbonBottomBorder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlRibbonBottomBorder.Name = "pnlRibbonBottomBorder";
            this.pnlRibbonBottomBorder.Size = new System.Drawing.Size(1360, 2);
            this.pnlRibbonBottomBorder.TabIndex = 16;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.lblTableTitle.Location = new System.Drawing.Point(24, 140);
            this.lblTableTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(1312, 30);
            this.lblTableTitle.TabIndex = 1;
            this.lblTableTitle.Text = "Реестр заявок на ремонт и техническое обслуживание";
            // 
            // dgvRequests
            // 
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToDeleteRows = false;
            this.dgvRequests.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.dgvRequests.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequests.BackgroundColor = System.Drawing.Color.White;
            this.dgvRequests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.dgvRequests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRequests.ColumnHeadersHeight = 36;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRequests.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRequests.EnableHeadersVisualStyles = false;
            this.dgvRequests.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            this.dgvRequests.Location = new System.Drawing.Point(24, 180);
            this.dgvRequests.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRequests.MultiSelect = false;
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersVisible = false;
            this.dgvRequests.RowHeadersWidth = 51;
            this.dgvRequests.RowTemplate.Height = 32;
            this.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequests.Size = new System.Drawing.Size(1312, 425);
            this.dgvRequests.TabIndex = 2;
            this.dgvRequests.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequests_CellDoubleClick);
            // 
            // pnlBottomActions
            // 
            this.pnlBottomActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlBottomActions.Controls.Add(this.btnCancelRequest);
            this.pnlBottomActions.Controls.Add(this.btnAssign);
            this.pnlBottomActions.Controls.Add(this.cmbAssignTech);
            this.pnlBottomActions.Controls.Add(this.lblAssignTitle);
            this.pnlBottomActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomActions.Location = new System.Drawing.Point(0, 621);
            this.pnlBottomActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBottomActions.Name = "pnlBottomActions";
            this.pnlBottomActions.Size = new System.Drawing.Size(1360, 69);
            this.pnlBottomActions.TabIndex = 3;
            // 
            // btnCancelRequest
            // 
            this.btnCancelRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(48)))), ((int)(((byte)(37)))));
            this.btnCancelRequest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelRequest.FlatAppearance.BorderSize = 0;
            this.btnCancelRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelRequest.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelRequest.ForeColor = System.Drawing.Color.White;
            this.btnCancelRequest.Location = new System.Drawing.Point(661, 16);
            this.btnCancelRequest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelRequest.Name = "btnCancelRequest";
            this.btnCancelRequest.Size = new System.Drawing.Size(207, 37);
            this.btnCancelRequest.TabIndex = 2;
            this.btnCancelRequest.Text = "Отменить заявку";
            this.btnCancelRequest.UseVisualStyleBackColor = false;
            this.btnCancelRequest.Click += new System.EventHandler(this.btnCancelRequest_Click);
            // 
            // btnAssign
            // 
            this.btnAssign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(142)))));
            this.btnAssign.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAssign.FlatAppearance.BorderSize = 0;
            this.btnAssign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssign.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAssign.ForeColor = System.Drawing.Color.White;
            this.btnAssign.Location = new System.Drawing.Point(469, 16);
            this.btnAssign.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(173, 37);
            this.btnAssign.TabIndex = 1;
            this.btnAssign.Text = "Назначить";
            this.btnAssign.UseVisualStyleBackColor = false;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // cmbAssignTech
            // 
            this.cmbAssignTech.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignTech.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbAssignTech.FormattingEnabled = true;
            this.cmbAssignTech.Location = new System.Drawing.Point(213, 18);
            this.cmbAssignTech.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAssignTech.Name = "cmbAssignTech";
            this.cmbAssignTech.Size = new System.Drawing.Size(239, 29);
            this.cmbAssignTech.TabIndex = 0;
            // 
            // lblAssignTitle
            // 
            this.lblAssignTitle.AutoSize = true;
            this.lblAssignTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAssignTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            this.lblAssignTitle.Location = new System.Drawing.Point(24, 23);
            this.lblAssignTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAssignTitle.Name = "lblAssignTitle";
            this.lblAssignTitle.Size = new System.Drawing.Size(149, 21);
            this.lblAssignTitle.TabIndex = 3;
            this.lblAssignTitle.Text = "Назначить техника:";
            // 
            // RepairRequestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1360, 690);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.lblTableTitle);
            this.Controls.Add(this.pnlBottomActions);
            this.Controls.Add(this.pnlRibbon);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1301, 605);
            this.Name = "RepairRequestsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Реестр заявок на ремонт";
            this.Load += new System.EventHandler(this.RepairRequestsForm_Load);
            this.pnlRibbon.ResumeLayout(false);
            this.pnlRibbon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.pnlBottomActions.ResumeLayout(false);
            this.pnlBottomActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRibbon;
        private System.Windows.Forms.Panel pnlRibbonBottomBorder;
        private System.Windows.Forms.Label lblCarNumber;
        private System.Windows.Forms.TextBox txtCarNumber;
        private System.Windows.Forms.Label lblGroupCar;
        private System.Windows.Forms.Panel sep1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.Label lblGroupParams;
        private System.Windows.Forms.Panel sep2;
        private System.Windows.Forms.Label lblTechnician;
        private System.Windows.Forms.TextBox txtTechnician;
        private System.Windows.Forms.Label lblGroupTech;
        private System.Windows.Forms.Panel sep3;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblGroupActions;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.Panel pnlBottomActions;
        private System.Windows.Forms.Label lblAssignTitle;
        private System.Windows.Forms.ComboBox cmbAssignTech;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnCancelRequest;
    }
}