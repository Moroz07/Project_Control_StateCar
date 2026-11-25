using System;
using System.Windows.Forms;
using CarsharingClassLibrary.Models;

namespace ControlStateCar
{
    public partial class RepairRequestDetailsForm : Form
    {
        private RepairRequestDetails details_;
        private RepairRequestsListModel model_;
        private User currentUser_;
        private bool isModified_ = false;

        public RepairRequestDetailsForm(RepairRequestDetails details, RepairRequestsListModel model = null, User currentUser = null)
        {
            InitializeComponent();
            details_ = details;
            model_ = model;
            currentUser_ = currentUser;

            SetupHistoryGrid();
            FillData();
            BindHistoryGrid();
        }

        private void SetupHistoryGrid()
        {
            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistDate",
                HeaderText = "Дата и время",
                DataPropertyName = "ChangedAtDisplay",
                FillWeight = 110,
                ReadOnly = true
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistStatus",
                HeaderText = "Статус / Событие",
                DataPropertyName = "StatusDisplay",
                FillWeight = 115,
                ReadOnly = true
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistEmployee",
                HeaderText = "Сотрудник",
                DataPropertyName = "EmployeeDisplay",
                FillWeight = 140,
                ReadOnly = true
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistComment",
                HeaderText = "Комментарий / Выполненные работы",
                DataPropertyName = "CommentDisplay",
                FillWeight = 260,
                ReadOnly = true
            });
        }

        private void BindHistoryGrid()
        {
            if (model_ == null || details_ == null)
            {
                dgvHistory.DataSource = null;
                return;
            }

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = model_.GetHistoryForGrid(details_.Id);
        }

        private int? GetCurrentEmployeeId()
        {
            if (currentUser_ == null)
            {
                return null;
            }

            try
            {
                var prop = currentUser_.GetType().GetProperty("Id");
                if (prop != null && prop.PropertyType == typeof(int))
                {
                    int id = (int)prop.GetValue(currentUser_);
                    return id > 0 ? (int?)id : null;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
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
                dtpClosedAt.Value = details_.ClosedAt.Value;
            }
            else
            {
                txtClosedAt.Text = "Не закрыта";
                dtpClosedAt.Value = DateTime.Now;
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

            // Блок 3: Заказчик
            txtCustomerName.Text = string.IsNullOrWhiteSpace(details_.CustomerName) ? "Не указан" : details_.CustomerName;
            txtCustomerPhone.Text = string.IsNullOrWhiteSpace(details_.CustomerPhone) ? "Не указан" : details_.CustomerPhone;

            // Блок 4: Ответственный специалист
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

            // Блок 5: Описание неисправности и выполненные работы
            txtDescription.Text = details_.Description ?? "";
            txtWorkDone.Text = details_.WorkDone ?? "";

            btnCloseRequest.Enabled = details_.Status != RepairStatus.Completed && details_.Status != RepairStatus.Cancelled;
        }

        private void btnCloseRequest_Click(object sender, EventArgs e)
        {
            if (model_ == null || details_ == null)
            {
                return;
            }

            DateTime closeDate = dtpClosedAt.Value;
            string workDoneText = txtWorkDone.Text;
            int? empId = GetCurrentEmployeeId();

            string error = model_.CloseRequest(details_.Id, workDoneText, closeDate, empId);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            details_.Status = RepairStatus.Completed;
            details_.ClosedAt = closeDate;
            details_.WorkDone = workDoneText.Trim();
            isModified_ = true;

            FillData();
            BindHistoryGrid();
            MessageBox.Show("Заявка успешно закрыта.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdateClosedDate_Click(object sender, EventArgs e)
        {
            if (model_ == null || details_ == null)
            {
                return;
            }

            DateTime newCloseDate = dtpClosedAt.Value;
            string workDoneText = txtWorkDone.Text;
            int? empId = GetCurrentEmployeeId();

            string error = model_.UpdateRequestClosedDate(details_.Id, newCloseDate, workDoneText, empId);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            details_.ClosedAt = newCloseDate;
            details_.WorkDone = workDoneText.Trim();
            isModified_ = true;

            FillData();
            BindHistoryGrid();
            MessageBox.Show("Дата закрытия успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = isModified_ ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }
    }
}