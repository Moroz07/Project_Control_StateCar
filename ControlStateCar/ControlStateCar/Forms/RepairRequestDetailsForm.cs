using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStateCar
{
    public partial class RepairRequestDetailsForm : Form
    {
        private RepairRequestDetails details_;

        public RepairRequestDetailsForm(RepairRequestDetails details)
        {
            InitializeComponent();
            details_ = details;

            FillData();
        }

        private void FillData()
        {
            if (details_ == null)
            {
                return;
            }

            this.Text = $"Карточка заявки №{details_.Id}";
            lblHeaderTitle.Text = $"Карточка заявки №{details_.Id}";

            // Блок 1: Параметры заявки
            txtRequestId.Text = details_.Id.ToString();
            txtCreatedAt.Text = details_.CreatedAt.ToString("dd.MM.yyyy HH:mm");

            if (details_.ClosedAt.HasValue)
            {
                txtClosedAt.Text = details_.ClosedAt.Value.ToString("dd.MM.yyyy HH:mm");
            }
            else
            {
                txtClosedAt.Text = "Не закрыта";
            }

            switch (details_.Status)
            {
                case RepairStatus.New:
                    txtStatus.Text = "новый";
                    break;
                case RepairStatus.InProgress:
                    txtStatus.Text = "в работе";
                    break;
                case RepairStatus.Completed:
                    txtStatus.Text = "завершён";
                    break;
                case RepairStatus.Cancelled:
                    txtStatus.Text = "отменена";
                    break;
                default:
                    txtStatus.Text = "новый";
                    break;
            }

            // Блок 2: Транспортное средство
            string fullCarName = (details_.CarBrand + " " + details_.CarModel).Trim();
            txtCarBrandModel.Text = fullCarName == "" ? "Не указан" : fullCarName;
            txtCarNumber.Text = details_.CarNumber ?? "Без номера";
            txtVin.Text = details_.Vin ?? "Не указан";
            txtYear.Text = details_.Year > 0 ? details_.Year.ToString() : "Не указан";

            // Блок 3: Ответственный специалист
            if (details_.TechnicianLogin == null || details_.TechnicianLogin.Trim() == "")
            {
                txtTechFullName.Text = "Не назначен";
                txtTechLogin.Text = "Не назначен";
                txtTechPhone.Text = "Не назначен";
            }
            else
            {
                txtTechFullName.Text = details_.TechnicianFullName ?? "Не указано";
                txtTechLogin.Text = details_.TechnicianLogin;
                txtTechPhone.Text = details_.TechnicianPhone ?? "Не указан";
            }

            // Блок 4: Описание неисправности
            txtDescription.Text = details_.Description ?? "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}