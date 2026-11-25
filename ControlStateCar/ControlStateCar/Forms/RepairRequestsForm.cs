using System;
using System.Windows.Forms;
using CarsharingClassLibrary.Models;

namespace ControlStateCar
{
    public partial class RepairRequestsForm : Form
    {
        private RepairRequestsListModel model_;
        private User currentUser_;

        public RepairRequestsForm(RepairRequestsListModel model, User currentUser = null)
        {
            InitializeComponent();
            model_ = model;
            currentUser_ = currentUser;

            InitializeFilterPanel();
            SetupDataGridView();
            LoadTechniciansToComboBox();

            if (currentUser_ != null)
            {
                if (currentUser_.Role == Role.техник)
                {
                    cmbAssignTech.Visible = false;
                    btnAssign.Visible = false;
                    btnCancelRequest.Location = cmbAssignTech.Location;
                }
            }
        }

        private void InitializeFilterPanel()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[]
            {
                "Все",
                "новый",
                "в работе",
                "завершён",
                "отменена"
            });
            cmbStatus.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvRequests.AutoGenerateColumns = false;
            dgvRequests.Columns.Clear();

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "Id",
                FillWeight = 40,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCarNumber",
                HeaderText = "Госномер",
                DataPropertyName = "CarNumber",
                FillWeight = 85,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescription",
                HeaderText = "Описание неисправности (двойной клик для карточки)",
                DataPropertyName = "Description",
                FillWeight = 230,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Статус",
                DataPropertyName = "StatusDisplay",
                FillWeight = 85,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCreatedAt",
                HeaderText = "Дата создания",
                DataPropertyName = "CreatedAtDisplay",
                FillWeight = 100,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTechnician",
                HeaderText = "Техник",
                DataPropertyName = "TechnicianDisplay",
                FillWeight = 90,
                ReadOnly = true
            });
        }

        private void LoadTechniciansToComboBox()
        {
            cmbAssignTech.DataSource = null;
            cmbAssignTech.Items.Clear();

            var techLogins = model_.GetTechnicianLogins();

            if (techLogins.Count > 0)
            {
                cmbAssignTech.DataSource = techLogins;
            }
            else
            {
                cmbAssignTech.Items.Add("Нет техников");
                cmbAssignTech.SelectedIndex = 0;
            }
        }

        private void RepairRequestsForm_Load(object sender, EventArgs e)
        {
            LoadDefaultRequests();
        }

        private void LoadDefaultRequests()
        {
            model_.LoadRecentRequests(15);
            BindGridDataSource();
        }

        private void BindGridDataSource()
        {
            dgvRequests.DataSource = null;
            dgvRequests.DataSource = model_.GetRequestsForGrid();
        }

        private void dgvRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var item = dgvRequests.Rows[e.RowIndex].DataBoundItem as RepairRequestGridItem;
            if (item == null)
            {
                return;
            }

            var details = model_.GetRequestDetails(item.Id);
            if (details == null)
            {
                MessageBox.Show(
                    "Ошибка: Заявка не найдена в базе данных",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            using (var detailsForm = new RepairRequestDetailsForm(details, model_))
            {
                if (detailsForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDefaultRequests();
                }
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку в таблице для назначения техника.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = dgvRequests.SelectedRows[0].DataBoundItem as RepairRequestGridItem;
            if (item == null)
            {
                return;
            }

            string selectedTechnician = "";
            if (cmbAssignTech.SelectedItem != null)
            {
                selectedTechnician = cmbAssignTech.SelectedItem.ToString();
            }

            string errorMessage = model_.AssignTechnicianToRequest(item.Id, selectedTechnician);

            if (errorMessage == null || errorMessage.Trim() == "")
            {
                MessageBox.Show("Техник успешно назначен, статус заявки изменен на 'в работе'.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDefaultRequests();
            }
            else
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelRequest_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку в таблице для отмены.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = dgvRequests.SelectedRows[0].DataBoundItem as RepairRequestGridItem;
            if (item == null)
            {
                return;
            }

            if (currentUser_ != null)
            {
                if (currentUser_.Role == Role.техник)
                {
                    if (item.TechnicianLogin != currentUser_.Login)
                    {
                        MessageBox.Show("Вы можете отменять только те заявки, которые назначены на вас.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            string errorMessage = model_.CancelRequest(item.Id);

            if (errorMessage == null || errorMessage.Trim() == "")
            {
                MessageBox.Show("Заявка успешно отменена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDefaultRequests();
            }
            else
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var filterParams = new RepairRequestFilterParameters();

            if (txtCarNumber.Text != null && txtCarNumber.Text.Trim() != "")
            {
                filterParams.CarNumber = txtCarNumber.Text.Trim();
            }

            if (cmbStatus.SelectedIndex > 0)
            {
                string selected = cmbStatus.SelectedItem.ToString();
                switch (selected)
                {
                    case "новый":
                        filterParams.Status = RepairStatus.New;
                        break;
                    case "в работе":
                        filterParams.Status = RepairStatus.InProgress;
                        break;
                    case "завершён":
                        filterParams.Status = RepairStatus.Completed;
                        break;
                    case "отменена":
                        filterParams.Status = RepairStatus.Cancelled;
                        break;
                }
            }

            if (txtTechnician.Text != null && txtTechnician.Text.Trim() != "")
            {
                filterParams.TechnicianLogin = txtTechnician.Text.Trim();
            }

            if (dtpFilterDate.Checked)
            {
                filterParams.CreatedDate = dtpFilterDate.Value.Date;
            }

            string resultMessage = model_.ApplyFilter(filterParams);
            BindGridDataSource();

            if (resultMessage != null && resultMessage.Trim() != "")
            {
                MessageBox.Show(resultMessage, "Результаты поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCarNumber.Clear();
            cmbStatus.SelectedIndex = 0;
            txtTechnician.Clear();
            dtpFilterDate.Checked = false;

            LoadDefaultRequests();
        }
    }
}