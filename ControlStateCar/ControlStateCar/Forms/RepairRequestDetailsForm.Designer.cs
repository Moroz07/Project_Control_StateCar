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

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBottomBorder;
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

            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomBorder = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlRequestCard.SuspendLayout();
            this.pnlCarCard.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(680, 74);
            this.pnlHeader.TabIndex = 0;

            this.pnlHeaderAccent.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.pnlHeaderAccent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderAccent.Location = new System.Drawing.Point(0, 71);
            this.pnlHeaderAccent.Name = "pnlHeaderAccent";
            this.pnlHeaderAccent.Size = new System.Drawing.Size(680, 3);

            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(22, 13);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(172, 25);
            this.lblHeaderTitle.Text = "Карточка заявки №";

            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(96, 165, 250);
            this.lblHeaderSub.Location = new System.Drawing.Point(24, 41);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(335, 15);
            this.lblHeaderSub.Text = "Подробные сведения о заявке, автомобиле и исполнителе";

            // 
            // pnlRequestCard
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
            this.pnlRequestCard.Location = new System.Drawing.Point(22, 90);
            this.pnlRequestCard.Name = "pnlRequestCard";
            this.pnlRequestCard.Size = new System.Drawing.Size(636, 98);
            this.pnlRequestCard.TabIndex = 1;

            this.pnlRequestCardTop.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.pnlRequestCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRequestCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlRequestCardTop.Name = "pnlRequestCardTop";
            this.pnlRequestCardTop.Size = new System.Drawing.Size(636, 3);

            this.lblRequestCardTitle.AutoSize = true;
            this.lblRequestCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblRequestCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblRequestCardTitle.Location = new System.Drawing.Point(16, 12);
            this.lblRequestCardTitle.Name = "lblRequestCardTitle";
            this.lblRequestCardTitle.Size = new System.Drawing.Size(128, 13);
            this.lblRequestCardTitle.Text = "ПАРАМЕТРЫ ЗАЯВКИ";

            this.lblRequestId.AutoSize = true;
            this.lblRequestId.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRequestId.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblRequestId.Location = new System.Drawing.Point(16, 36);
            this.lblRequestId.Name = "lblRequestId";
            this.lblRequestId.Size = new System.Drawing.Size(63, 15);
            this.lblRequestId.Text = "ID заявки:";

            this.txtRequestId.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtRequestId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRequestId.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtRequestId.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtRequestId.Location = new System.Drawing.Point(16, 54);
            this.txtRequestId.Name = "txtRequestId";
            this.txtRequestId.ReadOnly = true;
            this.txtRequestId.Size = new System.Drawing.Size(95, 24);
            this.txtRequestId.TabIndex = 0;

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblStatus.Location = new System.Drawing.Point(125, 36);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 15);
            this.lblStatus.Text = "Статус:";

            this.txtStatus.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtStatus.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.txtStatus.Location = new System.Drawing.Point(125, 54);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(150, 24);
            this.txtStatus.TabIndex = 1;

            this.lblCreatedAt.AutoSize = true;
            this.lblCreatedAt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCreatedAt.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCreatedAt.Location = new System.Drawing.Point(290, 36);
            this.lblCreatedAt.Name = "lblCreatedAt";
            this.lblCreatedAt.Size = new System.Drawing.Size(92, 15);
            this.lblCreatedAt.Text = "Дата создания:";

            this.txtCreatedAt.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCreatedAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreatedAt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCreatedAt.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCreatedAt.Location = new System.Drawing.Point(290, 54);
            this.txtCreatedAt.Name = "txtCreatedAt";
            this.txtCreatedAt.ReadOnly = true;
            this.txtCreatedAt.Size = new System.Drawing.Size(160, 24);
            this.txtCreatedAt.TabIndex = 2;

            this.lblClosedAt.AutoSize = true;
            this.lblClosedAt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblClosedAt.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblClosedAt.Location = new System.Drawing.Point(464, 36);
            this.lblClosedAt.Name = "lblClosedAt";
            this.lblClosedAt.Size = new System.Drawing.Size(92, 15);
            this.lblClosedAt.Text = "Дата закрытия:";

            this.txtClosedAt.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtClosedAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClosedAt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtClosedAt.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtClosedAt.Location = new System.Drawing.Point(464, 54);
            this.txtClosedAt.Name = "txtClosedAt";
            this.txtClosedAt.ReadOnly = true;
            this.txtClosedAt.Size = new System.Drawing.Size(156, 24);
            this.txtClosedAt.TabIndex = 3;

            // 
            // pnlCarCard
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
            this.pnlCarCard.Location = new System.Drawing.Point(22, 202);
            this.pnlCarCard.Name = "pnlCarCard";
            this.pnlCarCard.Size = new System.Drawing.Size(636, 98);
            this.pnlCarCard.TabIndex = 2;

            this.pnlCarCardTop.BackColor = System.Drawing.Color.FromArgb(99, 102, 241);
            this.pnlCarCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCarCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlCarCardTop.Name = "pnlCarCardTop";
            this.pnlCarCardTop.Size = new System.Drawing.Size(636, 3);

            this.lblCarCardTitle.AutoSize = true;
            this.lblCarCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCarCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCarCardTitle.Location = new System.Drawing.Point(16, 12);
            this.lblCarCardTitle.Name = "lblCarCardTitle";
            this.lblCarCardTitle.Size = new System.Drawing.Size(159, 13);
            this.lblCarCardTitle.Text = "ТРАНСПОРТНОЕ СРЕДСТВО";

            this.lblCarBrandModel.AutoSize = true;
            this.lblCarBrandModel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCarBrandModel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCarBrandModel.Location = new System.Drawing.Point(16, 36);
            this.lblCarBrandModel.Name = "lblCarBrandModel";
            this.lblCarBrandModel.Size = new System.Drawing.Size(96, 15);
            this.lblCarBrandModel.Text = "Марка и модель:";

            this.txtCarBrandModel.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCarBrandModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarBrandModel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCarBrandModel.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCarBrandModel.Location = new System.Drawing.Point(16, 54);
            this.txtCarBrandModel.Name = "txtCarBrandModel";
            this.txtCarBrandModel.ReadOnly = true;
            this.txtCarBrandModel.Size = new System.Drawing.Size(175, 24);
            this.txtCarBrandModel.TabIndex = 0;

            this.lblCarNumber.AutoSize = true;
            this.lblCarNumber.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCarNumber.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCarNumber.Location = new System.Drawing.Point(205, 36);
            this.lblCarNumber.Name = "lblCarNumber";
            this.lblCarNumber.Size = new System.Drawing.Size(65, 15);
            this.lblCarNumber.Text = "Госномер:";

            this.txtCarNumber.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtCarNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCarNumber.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCarNumber.Location = new System.Drawing.Point(205, 54);
            this.txtCarNumber.Name = "txtCarNumber";
            this.txtCarNumber.ReadOnly = true;
            this.txtCarNumber.Size = new System.Drawing.Size(115, 24);
            this.txtCarNumber.TabIndex = 1;

            this.lblVin.AutoSize = true;
            this.lblVin.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblVin.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVin.Location = new System.Drawing.Point(334, 36);
            this.lblVin.Name = "lblVin";
            this.lblVin.Size = new System.Drawing.Size(67, 15);
            this.lblVin.Text = "VIN-номер:";

            this.txtVin.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtVin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVin.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtVin.Location = new System.Drawing.Point(334, 54);
            this.txtVin.Name = "txtVin";
            this.txtVin.ReadOnly = true;
            this.txtVin.Size = new System.Drawing.Size(185, 24);
            this.txtVin.TabIndex = 2;

            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblYear.Location = new System.Drawing.Point(533, 36);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(76, 15);
            this.lblYear.Text = "Год выпуска:";

            this.txtYear.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtYear.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtYear.Location = new System.Drawing.Point(533, 54);
            this.txtYear.Name = "txtYear";
            this.txtYear.ReadOnly = true;
            this.txtYear.Size = new System.Drawing.Size(87, 24);
            this.txtYear.TabIndex = 3;

            // 
            // pnlTechCard
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
            this.pnlTechCard.Location = new System.Drawing.Point(22, 314);
            this.pnlTechCard.Name = "pnlTechCard";
            this.pnlTechCard.Size = new System.Drawing.Size(636, 98);
            this.pnlTechCard.TabIndex = 3;

            this.pnlTechCardTop.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            this.pnlTechCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTechCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTechCardTop.Name = "pnlTechCardTop";
            this.pnlTechCardTop.Size = new System.Drawing.Size(636, 3);

            this.lblTechCardTitle.AutoSize = true;
            this.lblTechCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTechCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTechCardTitle.Location = new System.Drawing.Point(16, 12);
            this.lblTechCardTitle.Name = "lblTechCardTitle";
            this.lblTechCardTitle.Size = new System.Drawing.Size(173, 13);
            this.lblTechCardTitle.Text = "ОТВЕТСТВЕННЫЙ СПЕЦИАЛИСТ";

            this.lblTechFullName.AutoSize = true;
            this.lblTechFullName.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTechFullName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTechFullName.Location = new System.Drawing.Point(16, 36);
            this.lblTechFullName.Name = "lblTechFullName";
            this.lblTechFullName.Size = new System.Drawing.Size(87, 15);
            this.lblTechFullName.Text = "ФИО техника:";

            this.txtTechFullName.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtTechFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTechFullName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTechFullName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTechFullName.Location = new System.Drawing.Point(16, 54);
            this.txtTechFullName.Name = "txtTechFullName";
            this.txtTechFullName.ReadOnly = true;
            this.txtTechFullName.Size = new System.Drawing.Size(270, 24);
            this.txtTechFullName.TabIndex = 0;

            this.lblTechLogin.AutoSize = true;
            this.lblTechLogin.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTechLogin.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTechLogin.Location = new System.Drawing.Point(300, 36);
            this.lblTechLogin.Name = "lblTechLogin";
            this.lblTechLogin.Size = new System.Drawing.Size(44, 15);
            this.lblTechLogin.Text = "Логин:";

            this.txtTechLogin.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtTechLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTechLogin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTechLogin.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTechLogin.Location = new System.Drawing.Point(300, 54);
            this.txtTechLogin.Name = "txtTechLogin";
            this.txtTechLogin.ReadOnly = true;
            this.txtTechLogin.Size = new System.Drawing.Size(140, 24);
            this.txtTechLogin.TabIndex = 1;

            this.lblTechPhone.AutoSize = true;
            this.lblTechPhone.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTechPhone.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTechPhone.Location = new System.Drawing.Point(454, 36);
            this.lblTechPhone.Name = "lblTechPhone";
            this.lblTechPhone.Size = new System.Drawing.Size(123, 15);
            this.lblTechPhone.Text = "Контактный телефон:";

            this.txtTechPhone.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtTechPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTechPhone.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTechPhone.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTechPhone.Location = new System.Drawing.Point(454, 54);
            this.txtTechPhone.Name = "txtTechPhone";
            this.txtTechPhone.ReadOnly = true;
            this.txtTechPhone.Size = new System.Drawing.Size(166, 24);
            this.txtTechPhone.TabIndex = 2;

            // 
            // pnlDescCard
            // 
            this.pnlDescCard.BackColor = System.Drawing.Color.White;
            this.pnlDescCard.Controls.Add(this.txtDescription);
            this.pnlDescCard.Controls.Add(this.lblDescCardTitle);
            this.pnlDescCard.Controls.Add(this.pnlDescCardTop);
            this.pnlDescCard.Location = new System.Drawing.Point(22, 426);
            this.pnlDescCard.Name = "pnlDescCard";
            this.pnlDescCard.Size = new System.Drawing.Size(636, 134);
            this.pnlDescCard.TabIndex = 4;

            this.pnlDescCardTop.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.pnlDescCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDescCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnlDescCardTop.Name = "pnlDescCardTop";
            this.pnlDescCardTop.Size = new System.Drawing.Size(636, 3);

            this.lblDescCardTitle.AutoSize = true;
            this.lblDescCardTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblDescCardTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDescCardTitle.Location = new System.Drawing.Point(16, 12);
            this.lblDescCardTitle.Name = "lblDescCardTitle";
            this.lblDescCardTitle.Size = new System.Drawing.Size(163, 13);
            this.lblDescCardTitle.Text = "ОПИСАНИЕ НЕИСПРАВНОСТИ";

            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtDescription.Location = new System.Drawing.Point(16, 34);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(604, 84);
            this.txtDescription.TabIndex = 0;

            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.pnlBottomBorder);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 578);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(680, 62);
            this.pnlBottom.TabIndex = 5;

            this.pnlBottomBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlBottomBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBottomBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlBottomBorder.Name = "pnlBottomBorder";
            this.pnlBottomBorder.Size = new System.Drawing.Size(680, 1);

            this.btnClose.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(518, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // RepairRequestDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(680, 640);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlDescCard);
            this.Controls.Add(this.pnlTechCard);
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
            this.pnlTechCard.ResumeLayout(false);
            this.pnlTechCard.PerformLayout();
            this.pnlDescCard.ResumeLayout(false);
            this.pnlDescCard.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}