using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CarsharingClassLibrary.Models;
using Npgsql;

namespace ControlStateCar
{
    public partial class RepairRequestsForm : Form
    {
        private  RepairRequestsListModel model_;
        private  User currentUser_;

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
            dgvRequests.Columns.Clear();

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                FillWeight = 40,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCarNumber",
                HeaderText = "Госномер",
                FillWeight = 85,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescription",
                HeaderText = "Описание неисправности (двойной клик для полного текста)",
                FillWeight = 230,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Статус",
                FillWeight = 85,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCreatedAt",
                HeaderText = "Дата создания",
                FillWeight = 100,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTechnician",
                HeaderText = "Техник",
                FillWeight = 90,
                ReadOnly = true
            });
        }

        private void LoadTechniciansToComboBox()
        {
            cmbAssignTech.Items.Clear();
            var techLogins = new List<string>();

            try
            {
                var conn = DBConnection.GetOpenConnection();
                string sql = "SELECT login FROM employees WHERE role = 'техник' OR role = 'technician' ORDER BY full_name ASC";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        techLogins.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                // Игнорируем при отсутствии соединения
            }

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
            DisplayRequests(model_.Requests);
        }

        private void DisplayRequests(List<RepairRequest> requests)
        {
            dgvRequests.Rows.Clear();

            if (requests == null)
            {
                return;
            }

            foreach (var req in requests)
            {
                string statusDisplay;
                switch (req.Status)
                {
                    case RepairStatus.New:
                        statusDisplay = "новый";
                        break;
                    case RepairStatus.InProgress:
                        statusDisplay = "в работе";
                        break;
                    case RepairStatus.Completed:
                        statusDisplay = "завершён";
                        break;
                    case RepairStatus.Cancelled:
                        statusDisplay = "отменена";
                        break;
                    default:
                        statusDisplay = "новый";
                        break;
                }

                string techDisplay;
                if (req.TechnicianLogin == null || req.TechnicianLogin.Trim() == "")
                {
                    techDisplay = "Не назначен";
                }
                else
                {
                    techDisplay = req.TechnicianLogin;
                }

                int rowIndex = dgvRequests.Rows.Add(
                    req.Id,
                    req.CarNumber,
                    req.Description,
                    statusDisplay,
                    req.CreatedAt.ToString("dd.MM.yyyy HH:mm"),
                    techDisplay
                );

                dgvRequests.Rows[rowIndex].Tag = req;
            }
        }

        private void dgvRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var req = dgvRequests.Rows[e.RowIndex].Tag as RepairRequest;
            if (req != null)
            {
                MessageBox.Show(
                    req.Description,
                    $"Полное описание заявки №{req.Id} ({req.CarNumber})",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку в таблице для назначения техника.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var request = dgvRequests.SelectedRows[0].Tag as RepairRequest;
            if (request == null)
            {
                return;
            }

            string selectedTechnician = "";
            if (cmbAssignTech.SelectedItem != null)
            {
                selectedTechnician = cmbAssignTech.SelectedItem.ToString();
            }

            string errorMessage = model_.AssignTechnicianToRequest(request.Id, selectedTechnician);

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

            var request = dgvRequests.SelectedRows[0].Tag as RepairRequest;
            if (request == null)
            {
                return;
            }

            if (currentUser_ != null)
            {
                if (currentUser_.Role == Role.техник)
                {
                    if (request.TechnicianLogin != currentUser_.Login)
                    {
                        MessageBox.Show("Вы можете отменять только те заявки, которые назначены на вас.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            string errorMessage = model_.CancelRequest(request.Id);

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
            DisplayRequests(model_.Requests);

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