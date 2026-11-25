namespace ControlStateCar
{
    partial class RepairRequestDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderAccent;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;

        private System.Windows.Forms.Panel pnlRequestCard;
        private System.Windows.Forms.Panel pnlRequestCardTop;
        private System.Windows.Forms.Label lblRequestCardTitle;
        private System.Windows.Forms.Label lblRequestId;
        private System.Windows.Forms.TextBox txtRequestId;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblCreatedAt;
        private System.Windows.Forms.TextBox txtCreatedAt;
        private System.Windows.Forms.Label lblClosedAt;
        private System.Windows.Forms.TextBox txtClosedAt;

        private System.Windows.Forms.Panel pnlCarCard;
        private System.Windows.Forms.Panel pnlCarCardTop;
        private System.Windows.Forms.Label lblCarCardTitle;
        private System.Windows.Forms.Label lblCarBrandModel;
        private System.Windows.Forms.TextBox txtCarBrandModel;
        private System.Windows.Forms.Label lblCarNumber;
        private System.Windows.Forms.TextBox txtCarNumber;
        private System.Windows.Forms.Label lblVin;
        private System.Windows.Forms.TextBox txtVin;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtYear;

        private System.Windows.Forms.Panel pnlCustomerCard;
        private System.Windows.Forms.Panel pnlCustomerCardTop;
        private System.Windows.Forms.Label lblCustomerCardTitle;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblCustomerPhone;
        private System.Windows.Forms.TextBox txtCustomerPhone;

        private System.Windows.Forms.Panel pnlTechCard;
        private System.Windows.Forms.Panel pnlTechCardTop;
        private System.Windows.Forms.Label lblTechCardTitle;
        private System.Windows.Forms.Label lblTechFullName;
        private System.Windows.Forms.TextBox txtTechFullName;
        private System.Windows.Forms.Label lblTechLogin;
        private System.Windows.Forms.TextBox txtTechLogin;
        private System.Windows.Forms.Label lblTechPhone;
        private System.Windows.Forms.TextBox txtTechPhone;

        private System.Windows.Forms.Panel pnlDescCard;
        private System.Windows.Forms.Panel pnlDescCardTop;
        private System.Windows.Forms.Label lblDescCardTitle;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblWorkDoneTitle;
        private System.Windows.Forms.TextBox txtWorkDone;

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBottomBorder;
        private System.Windows.Forms.Label lblNewCloseDate;
        private System.Windows.Forms.DateTimePicker dtpClosedAt;
        private System.Windows.Forms.Button btnUpdateClosedDate;
        private System.Windows.Forms.Button btnCloseRequest;
        private System.Windows.Forms.Button btnClose;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderAccent = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();

            this.pnlRequestCard = new System.Windows.Forms.Panel();
            this.pnlRequestCardTop = new System.Windows.Forms.Panel();
            this.lblRequestCardTitle = new System.Windows.Forms.Label();
            this.lblRequestId = new System.Windows.Forms.Label();
            this.txtRequestId = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblCreatedAt = new System.Windows.Forms.Label();
            this.txtCreatedAt = new System.Windows.Forms.TextBox();
            this.lblClosedAt = new System.Windows.Forms.Label();
            this.txtClosedAt = new System.Windows.Forms.TextBox();

            this.pnlCarCard = new System.Windows.Forms.Panel();
            this.pnlCarCardTop = new System.Windows.Forms.Panel();
            this.lblCarCardTitle = new System.Windows.Forms.Label();
            this.lblCarBrandModel = new System.Windows.Forms.Label();
            this.txtCarBrandModel = new System.Windows.Forms.TextBox();
            this.lblCarNumber = new System.Windows.Forms.Label();
            this.txtCarNumber = new System.Windows.Forms.TextBox();
            this.lblVin = new System.Windows.Forms.Label();
            this.txtVin = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();

            this.pnlCustomerCard = new System.Windows.Forms.Panel();
            this.pnlCustomerCardTop = new System.Windows.Forms.Panel();
            this.lblCustomerCardTitle = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblCustomerPhone = new System.Windows.Forms.Label();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();

            this.pnlTechCard = new System.Windows.Forms.Panel();
            this.pnlTechCardTop = new System.Windows.Forms.Panel();
            this.lblTechCardTitle = new System.Windows.Forms.Label();
            this.lblTechFullName = new System.Windows.Forms.Label();
            this.txtTechFullName = new System.Windows.Forms.TextBox();
            this.lblTechLogin = new System.Windows.Forms.Label();
            this.txtTechLogin = new System.Windows.Forms.TextBox();
            this.lblTechPhone = new System.Windows.Forms.Label();
            this.txtTechPhone = new System.Windows.Forms.TextBox();

            this.pnlDescCard = new System.Windows.Forms.Panel();
            this.pnlDescCardTop = new System.Windows.Forms.Panel();
            this.lblDescCardTitle = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblWorkDoneTitle = new System.Windows.Forms.Label();
            this.txtWorkDone = new System.Windows.Forms.TextBox();

            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomBorder = new System.Windows.Forms.Panel();
            this.lblNewCloseDate = new System.Windows.Forms.Label();
            this.dtpClosedAt = new System.Windows.Forms.DateTimePicker();
            this.btnUpdateClosedDate = new System.Windows.Forms.Button();
            this.btnCloseRequest = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlRequestCard.SuspendLayout();
            this.pnlCarCard.SuspendLayout();
            this.pnlCustomerCard.SuspendLayout();
            this.pnlTechCard.SuspendLayout();
            this.pnlDescCard.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(17, 20, 28);
            this.pnlHeader.Controls.Add(this.lblHeaderSub);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.pnlHeaderAccent);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(720, 68);
            this.pnlHeader.TabIndex = 0;

            this.pnlHeaderAccent.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.pnlHeaderAccent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderAccent.Location = new System.Drawing.Point(0, 65);
            this.pnlHeaderAccent.Name = "pnlHeaderAccent";
            this.pnlHeaderAccent.Size = new System.Drawing.Size(720, 3);

            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(22, 11);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(172, 25);
            this.lblHeaderTitle.Text = "Карточка заявки №";

            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(96, 165, 250);
            this.lblHeaderSub.Location = new System.Drawing.Point(24, 38);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(375, 15);
            this.lblHeaderSub.Text = "Полная информация о заявке, автомобиле, заказчике и выполнении";

            // 
            // pnlRequestCard (Блок 1: Параметры заявки)
            // 
            this.pnlRequestCard.BackColor = System.Drawing.Color.White;
            this.pnlRequestCard.Controls.Add(this.txtClosedAt);
            this.pnlRequestCard.Controls.Add(this.lblClosedAt);
            this.pnlRequestCard.Controls.Add(this.txtCreatedAt);
            this.pnlRequestCard.Controls.Add(this.lblCreatedAt);
            this.pnlRequestCard.Controls.Add(this.txtStatus);
            this.pnlRequestCard.Controls.Add(this.lblStatus);
            this.pnlRequestCard.Controls.Add(this.txtRequestId);
            this.pnlRequestCard.Controls.Add(this.lblRequestId);
            this.pnlRequestCard.Controls.Add(this.lblRequestCardTitle);
            this.pnlRequestCard.Controls.Add(this.pnlRequestCardTop);
            this.pnlRequestCard.Location = new System.Drawing.Point(22, 82);
            this.pnlRequestCard.Name = "pnlRequestCard";
            this.pnlRequestCard.Size = new System.Drawing.Size(676, 90);
            this.pnlRequestCard.TabIndex = 1;

            this.pnlRequestCardTop.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.pnlRequestCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRequestCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlRequestCardTop.Name = "pnlRequestCardTop";
            this.pnlRequestCardTop.Size = new System.Drawing.Size(676, 3);

            this.lblRequestCardTitle.AutoSize = true;
            this.lblRequestCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblRequestCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblRequestCardTitle.Location = new System.Drawing.Point(16, 10);
            this.lblRequestCardTitle.Name = "lblRequestCardTitle";
            this.lblRequestCardTitle.Size = new System.Drawing.Size(128, 13);
            this.lblRequestCardTitle.Text = "ПАРАМЕТРЫ ЗАЯВКИ";

            this.lblRequestId.AutoSize = true;
            this.lblRequestId.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRequestId.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblRequestId.Location = new System.Drawing.Point(16, 31);
            this.lblRequestId.Name = "lblRequestId";
            this.lblRequestId.Size = new System.Drawing.Size(63, 15);
            this.lblRequestId.Text = "ID заявки:";

            this.txtRequestId.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtRequestId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRequestId.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtRequestId.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtRequestId.Location = new System.Drawing.Point(16, 49);
            this.txtRequestId.Name = "txtRequestId";
            this.txtRequestId.ReadOnly = true;
            this.txtRequestId.Size = new System.Drawing.Size(95, 24);
            this.txtRequestId.TabIndex = 0;

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblStatus.Location = new System.Drawing.Point(125, 31);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 15);
            this.lblStatus.Text = "Статус:";

            this.txtStatus.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtStatus.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.txtStatus.Location = new System.Drawing.Point(125, 49);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(160, 24);
            this.txtStatus.TabIndex = 1;

            this.lblCreatedAt.AutoSize = true;
            this.lblCreatedAt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCreatedAt.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCreatedAt.Location = new System.Drawing.Point(300, 31);
            this.lblCreatedAt.Name = "lblCreatedAt";
            this.lblCreatedAt.Size = new System.Drawing.Size(92, 15);
            this.lblCreatedAt.Text = "Дата создания:";

            this.txtCreatedAt.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCreatedAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreatedAt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCreatedAt.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCreatedAt.Location = new System.Drawing.Point(300, 49);
            this.txtCreatedAt.Name = "txtCreatedAt";
            this.txtCreatedAt.ReadOnly = true;
            this.txtCreatedAt.Size = new System.Drawing.Size(175, 24);
            this.txtCreatedAt.TabIndex = 2;

            this.lblClosedAt.AutoSize = true;
            this.lblClosedAt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblClosedAt.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblClosedAt.Location = new System.Drawing.Point(489, 31);
            this.lblClosedAt.Name = "lblClosedAt";
            this.lblClosedAt.Size = new System.Drawing.Size(92, 15);
            this.lblClosedAt.Text = "Дата закрытия:";

            this.txtClosedAt.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtClosedAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClosedAt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtClosedAt.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtClosedAt.Location = new System.Drawing.Point(489, 49);
            this.txtClosedAt.Name = "txtClosedAt";
            this.txtClosedAt.ReadOnly = true;
            this.txtClosedAt.Size = new System.Drawing.Size(171, 24);
            this.txtClosedAt.TabIndex = 3;

            // 
            // pnlCarCard (Блок 2: Транспортное средство)
            // 
            this.pnlCarCard.BackColor = System.Drawing.Color.White;
            this.pnlCarCard.Controls.Add(this.txtYear);
            this.pnlCarCard.Controls.Add(this.lblYear);
            this.pnlCarCard.Controls.Add(this.txtVin);
            this.pnlCarCard.Controls.Add(this.lblVin);
            this.pnlCarCard.Controls.Add(this.txtCarNumber);
            this.pnlCarCard.Controls.Add(this.lblCarNumber);
            this.pnlCarCard.Controls.Add(this.txtCarBrandModel);
            this.pnlCarCard.Controls.Add(this.lblCarBrandModel);
            this.pnlCarCard.Controls.Add(this.lblCarCardTitle);
            this.pnlCarCard.Controls.Add(this.pnlCarCardTop);
            this.pnlCarCard.Location = new System.Drawing.Point(22, 182);
            this.pnlCarCard.Name = "pnlCarCard";
            this.pnlCarCard.Size = new System.Drawing.Size(676, 90);
            this.pnlCarCard.TabIndex = 2;

            this.pnlCarCardTop.BackColor = System.Drawing.Color.FromArgb(99, 102, 241);
            this.pnlCarCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCarCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlCarCardTop.Name = "pnlCarCardTop";
            this.pnlCarCardTop.Size = new System.Drawing.Size(676, 3);

            this.lblCarCardTitle.AutoSize = true;
            this.lblCarCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCarCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCarCardTitle.Location = new System.Drawing.Point(16, 10);
            this.lblCarCardTitle.Name = "lblCarCardTitle";
            this.lblCarCardTitle.Size = new System.Drawing.Size(159, 13);
            this.lblCarCardTitle.Text = "ТРАНСПОРТНОЕ СРЕДСТВО";

            this.lblCarBrandModel.AutoSize = true;
            this.lblCarBrandModel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCarBrandModel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCarBrandModel.Location = new System.Drawing.Point(16, 31);
            this.lblCarBrandModel.Name = "lblCarBrandModel";
            this.lblCarBrandModel.Size = new System.Drawing.Size(96, 15);
            this.lblCarBrandModel.Text = "Марка и модель:";

            this.txtCarBrandModel.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCarBrandModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarBrandModel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCarBrandModel.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCarBrandModel.Location = new System.Drawing.Point(16, 49);
            this.txtCarBrandModel.Name = "txtCarBrandModel";
            this.txtCarBrandModel.ReadOnly = true;
            this.txtCarBrandModel.Size = new System.Drawing.Size(185, 24);
            this.txtCarBrandModel.TabIndex = 0;

            this.lblCarNumber.AutoSize = true;
            this.lblCarNumber.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCarNumber.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCarNumber.Location = new System.Drawing.Point(215, 31);
            this.lblCarNumber.Name = "lblCarNumber";
            this.lblCarNumber.Size = new System.Drawing.Size(65, 15);
            this.lblCarNumber.Text = "Госномер:";

            this.txtCarNumber.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCarNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCarNumber.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCarNumber.Location = new System.Drawing.Point(215, 49);
            this.txtCarNumber.Name = "txtCarNumber";
            this.txtCarNumber.ReadOnly = true;
            this.txtCarNumber.Size = new System.Drawing.Size(120, 24);
            this.txtCarNumber.TabIndex = 1;

            this.lblVin.AutoSize = true;
            this.lblVin.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblVin.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVin.Location = new System.Drawing.Point(349, 31);
            this.lblVin.Name = "lblVin";
            this.lblVin.Size = new System.Drawing.Size(67, 15);
            this.lblVin.Text = "VIN-номер:";

            this.txtVin.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtVin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVin.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtVin.Location = new System.Drawing.Point(349, 49);
            this.txtVin.Name = "txtVin";
            this.txtVin.ReadOnly = true;
            this.txtVin.Size = new System.Drawing.Size(205, 24);
            this.txtVin.TabIndex = 2;

            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblYear.Location = new System.Drawing.Point(568, 31);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(76, 15);
            this.lblYear.Text = "Год выпуска:";

            this.txtYear.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtYear.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtYear.Location = new System.Drawing.Point(568, 49);
            this.txtYear.Name = "txtYear";
            this.txtYear.ReadOnly = true;
            this.txtYear.Size = new System.Drawing.Size(92, 24);
            this.txtYear.TabIndex = 3;

            // 
            // pnlCustomerCard (Блок 3: Информация о заказчике)
            // 
            this.pnlCustomerCard.BackColor = System.Drawing.Color.White;
            this.pnlCustomerCard.Controls.Add(this.txtCustomerPhone);
            this.pnlCustomerCard.Controls.Add(this.lblCustomerPhone);
            this.pnlCustomerCard.Controls.Add(this.txtCustomerName);
            this.pnlCustomerCard.Controls.Add(this.lblCustomerName);
            this.pnlCustomerCard.Controls.Add(this.lblCustomerCardTitle);
            this.pnlCustomerCard.Controls.Add(this.pnlCustomerCardTop);
            this.pnlCustomerCard.Location = new System.Drawing.Point(22, 282);
            this.pnlCustomerCard.Name = "pnlCustomerCard";
            this.pnlCustomerCard.Size = new System.Drawing.Size(676, 90);
            this.pnlCustomerCard.TabIndex = 3;

            this.pnlCustomerCardTop.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.pnlCustomerCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomerCardTop.Name = "pnlCustomerCardTop";
            this.pnlCustomerCardTop.Size = new System.Drawing.Size(676, 3);

            this.lblCustomerCardTitle.AutoSize = true;
            this.lblCustomerCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCustomerCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCustomerCardTitle.Location = new System.Drawing.Point(16, 10);
            this.lblCustomerCardTitle.Name = "lblCustomerCardTitle";
            this.lblCustomerCardTitle.Size = new System.Drawing.Size(156, 13);
            this.lblCustomerCardTitle.Text = "ИНФОРМАЦИЯ О ЗАКАЗЧИКЕ";

            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCustomerName.Location = new System.Drawing.Point(16, 31);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(151, 15);
            this.lblCustomerName.Text = "ФИО / Наименование:";

            this.txtCustomerName.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCustomerName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCustomerName.Location = new System.Drawing.Point(16, 49);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(410, 24);
            this.txtCustomerName.TabIndex = 0;

            this.lblCustomerPhone.AutoSize = true;
            this.lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCustomerPhone.Location = new System.Drawing.Point(442, 31);
            this.lblCustomerPhone.Name = "lblCustomerPhone";
            this.lblCustomerPhone.Size = new System.Drawing.Size(123, 15);
            this.lblCustomerPhone.Text = "Контактный телефон:";

            this.txtCustomerPhone.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCustomerPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCustomerPhone.Location = new System.Drawing.Point(442, 49);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.ReadOnly = true;
            this.txtCustomerPhone.Size = new System.Drawing.Size(218, 24);
            this.txtCustomerPhone.TabIndex = 1;

            // 
            // pnlTechCard (Блок 4: Ответственный специалист)
            // 
            this.pnlTechCard.BackColor = System.Drawing.Color.White;
            this.pnlTechCard.Controls.Add(this.txtTechPhone);
            this.pnlTechCard.Controls.Add(this.lblTechPhone);
            this.pnlTechCard.Controls.Add(this.txtTechLogin);
            this.pnlTechCard.Controls.Add(this.lblTechLogin);
            this.pnlTechCard.Controls.Add(this.txtTechFullName);
            this.pnlTechCard.Controls.Add(this.lblTechFullName);
            this.pnlTechCard.Controls.Add(this.lblTechCardTitle);
            this.pnlTechCard.Controls.Add(this.pnlTechCardTop);
            this.pnlTechCard.Location = new System.Drawing.Point(22, 382);
            this.pnlTechCard.Name = "pnlTechCard";
            this.pnlTechCard.Size = new System.Drawing.Size(676, 90);
            this.pnlTechCard.TabIndex = 4;

            this.pnlTechCardTop.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            this.pnlTechCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTechCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTechCardTop.Name = "pnlTechCardTop";
            this.pnlTechCardTop.Size = new System.Drawing.Size(676, 3);

            this.lblTechCardTitle.AutoSize = true;
            this.lblTechCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTechCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTechCardTitle.Location = new System.Drawing.Point(16, 10);
            this.lblTechCardTitle.Name = "lblTechCardTitle";
            this.lblTechCardTitle.Size = new System.Drawing.Size(173, 13);
            this.lblTechCardTitle.Text = "ОТВЕТСТВЕННЫЙ СПЕЦИАЛИСТ";

            this.lblTechFullName.AutoSize = true;
            this.lblTechFullName.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTechFullName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTechFullName.Location = new System.Drawing.Point(16, 31);
            this.lblTechFullName.Name = "lblTechFullName";
            this.lblTechFullName.Size = new System.Drawing.Size(87, 15);
            this.lblTechFullName.Text = "ФИО техника:";

            this.txtTechFullName.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtTechFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTechFullName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTechFullName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTechFullName.Location = new System.Drawing.Point(16, 49);
            this.txtTechFullName.Name = "txtTechFullName";
            this.txtTechFullName.ReadOnly = true;
            this.txtTechFullName.Size = new System.Drawing.Size(290, 24);
            this.txtTechFullName.TabIndex = 0;

            this.lblTechLogin.AutoSize = true;
            this.lblTechLogin.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTechLogin.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTechLogin.Location = new System.Drawing.Point(320, 31);
            this.lblTechLogin.Name = "lblTechLogin";
            this.lblTechLogin.Size = new System.Drawing.Size(44, 15);
            this.lblTechLogin.Text = "Логин:";

            this.txtTechLogin.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtTechLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTechLogin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTechLogin.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTechLogin.Location = new System.Drawing.Point(320, 49);
            this.txtTechLogin.Name = "txtTechLogin";
            this.txtTechLogin.ReadOnly = true;
            this.txtTechLogin.Size = new System.Drawing.Size(150, 24);
            this.txtTechLogin.TabIndex = 1;

            this.lblTechPhone.AutoSize = true;
            this.lblTechPhone.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTechPhone.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTechPhone.Location = new System.Drawing.Point(484, 31);
            this.lblTechPhone.Name = "lblTechPhone";
            this.lblTechPhone.Size = new System.Drawing.Size(123, 15);
            this.lblTechPhone.Text = "Контактный телефон:";

            this.txtTechPhone.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtTechPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTechPhone.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTechPhone.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTechPhone.Location = new System.Drawing.Point(484, 49);
            this.txtTechPhone.Name = "txtTechPhone";
            this.txtTechPhone.ReadOnly = true;
            this.txtTechPhone.Size = new System.Drawing.Size(176, 24);
            this.txtTechPhone.TabIndex = 2;

            // 
            // pnlDescCard (Блок 5: Неисправность и выполненные работы)
            // 
            this.pnlDescCard.BackColor = System.Drawing.Color.White;
            this.pnlDescCard.Controls.Add(this.txtWorkDone);
            this.pnlDescCard.Controls.Add(this.lblWorkDoneTitle);
            this.pnlDescCard.Controls.Add(this.txtDescription);
            this.pnlDescCard.Controls.Add(this.lblDescCardTitle);
            this.pnlDescCard.Controls.Add(this.pnlDescCardTop);
            this.pnlDescCard.Location = new System.Drawing.Point(22, 482);
            this.pnlDescCard.Name = "pnlDescCard";
            this.pnlDescCard.Size = new System.Drawing.Size(676, 155);
            this.pnlDescCard.TabIndex = 5;

            this.pnlDescCardTop.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.pnlDescCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDescCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlDescCardTop.Name = "pnlDescCardTop";
            this.pnlDescCardTop.Size = new System.Drawing.Size(676, 3);

            this.lblDescCardTitle.AutoSize = true;
            this.lblDescCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblDescCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDescCardTitle.Location = new System.Drawing.Point(16, 10);
            this.lblDescCardTitle.Name = "lblDescCardTitle";
            this.lblDescCardTitle.Size = new System.Drawing.Size(163, 13);
            this.lblDescCardTitle.Text = "ОПИСАНИЕ НЕИСПРАВНОСТИ";

            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtDescription.Location = new System.Drawing.Point(16, 30);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(312, 110);
            this.txtDescription.TabIndex = 0;

            this.lblWorkDoneTitle.AutoSize = true;
            this.lblWorkDoneTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblWorkDoneTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblWorkDoneTitle.Location = new System.Drawing.Point(344, 10);
            this.lblWorkDoneTitle.Name = "lblWorkDoneTitle";
            this.lblWorkDoneTitle.Size = new System.Drawing.Size(218, 13);
            this.lblWorkDoneTitle.Text = "ВЫПОЛНЕННЫЕ РАБОТЫ (ОТЧЁТ ТЕХНИКА)";

            this.txtWorkDone.BackColor = System.Drawing.Color.White;
            this.txtWorkDone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWorkDone.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtWorkDone.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtWorkDone.Location = new System.Drawing.Point(344, 30);
            this.txtWorkDone.Multiline = true;
            this.txtWorkDone.Name = "txtWorkDone";
            this.txtWorkDone.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWorkDone.Size = new System.Drawing.Size(316, 110);
            this.txtWorkDone.TabIndex = 1;

            // 
            // pnlBottom (Нижняя панель управления заявкой)
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnCloseRequest);
            this.pnlBottom.Controls.Add(this.btnUpdateClosedDate);
            this.pnlBottom.Controls.Add(this.dtpClosedAt);
            this.pnlBottom.Controls.Add(this.lblNewCloseDate);
            this.pnlBottom.Controls.Add(this.pnlBottomBorder);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 650);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(720, 72);
            this.pnlBottom.TabIndex = 6;

            this.pnlBottomBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlBottomBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBottomBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlBottomBorder.Name = "pnlBottomBorder";
            this.pnlBottomBorder.Size = new System.Drawing.Size(720, 1);

            this.lblNewCloseDate.AutoSize = true;
            this.lblNewCloseDate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNewCloseDate.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblNewCloseDate.Location = new System.Drawing.Point(22, 8);
            this.lblNewCloseDate.Name = "lblNewCloseDate";
            this.lblNewCloseDate.Size = new System.Drawing.Size(118, 13);
            this.lblNewCloseDate.Text = "Дата и время закрытия:";

            this.dtpClosedAt.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpClosedAt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpClosedAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpClosedAt.Location = new System.Drawing.Point(22, 26);
            this.dtpClosedAt.Name = "dtpClosedAt";
            this.dtpClosedAt.Size = new System.Drawing.Size(145, 23);
            this.dtpClosedAt.TabIndex = 0;

            this.btnUpdateClosedDate.BackColor = System.Drawing.Color.White;
            this.btnUpdateClosedDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateClosedDate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnUpdateClosedDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateClosedDate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnUpdateClosedDate.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnUpdateClosedDate.Location = new System.Drawing.Point(175, 19);
            this.btnUpdateClosedDate.Name = "btnUpdateClosedDate";
            this.btnUpdateClosedDate.Size = new System.Drawing.Size(175, 36);
            this.btnUpdateClosedDate.TabIndex = 1;
            this.btnUpdateClosedDate.Text = "Изменить дату закрытия";
            this.btnUpdateClosedDate.UseVisualStyleBackColor = false;
            this.btnUpdateClosedDate.Click += new System.EventHandler(this.btnUpdateClosedDate_Click);

            this.btnCloseRequest.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnCloseRequest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseRequest.FlatAppearance.BorderSize = 0;
            this.btnCloseRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseRequest.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnCloseRequest.ForeColor = System.Drawing.Color.White;
            this.btnCloseRequest.Location = new System.Drawing.Point(360, 19);
            this.btnCloseRequest.Name = "btnCloseRequest";
            this.btnCloseRequest.Size = new System.Drawing.Size(145, 36);
            this.btnCloseRequest.TabIndex = 2;
            this.btnCloseRequest.Text = "Закрыть заявку";
            this.btnCloseRequest.UseVisualStyleBackColor = false;
            this.btnCloseRequest.Click += new System.EventHandler(this.btnCloseRequest_Click);

            this.btnClose.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(578, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 36);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Выход";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // RepairRequestDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(720, 722);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlDescCard);
            this.Controls.Add(this.pnlTechCard);
            this.Controls.Add(this.pnlCustomerCard);
            this.Controls.Add(this.pnlCarCard);
            this.Controls.Add(this.pnlRequestCard);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepairRequestDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Карточка заявки";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlRequestCard.ResumeLayout(false);
            this.pnlRequestCard.PerformLayout();
            this.pnlCarCard.ResumeLayout(false);
            this.pnlCarCard.PerformLayout();
            this.pnlCustomerCard.ResumeLayout(false);
            this.pnlCustomerCard.PerformLayout();
            this.pnlTechCard.ResumeLayout(false);
            this.pnlTechCard.PerformLayout();
            this.pnlDescCard.ResumeLayout(false);
            this.pnlDescCard.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}