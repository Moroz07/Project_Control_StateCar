using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

namespace ControlStateCar
{
    public partial class RepairRequestsForm : Form
    {
        private  RepairRequestsListModel _model;
        private  User _currentUser;

        public RepairRequestsForm(RepairRequestsListModel model, User currentUser = null)
        {
            InitializeComponent();
            _model = model;
            _currentUser = currentUser;

            InitializeFilterPanel();
            SetupDataGridView();
            LoadTechniciansToComboBox();

            if (_currentUser != null && _currentUser.Role == Role.техник)
            {
                cmbAssignTech.Visible = false;
                btnAssign.Visible = false;

                btnCancelRequest.Location = btnAssign.Location;
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
                FillWeight = 80,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescription",
                HeaderText = "Описание неисправности",
                FillWeight = 190,
                ReadOnly = true
            });

            dgvRequests.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colMore",
                HeaderText = "+",
                Text = "+",
                UseColumnTextForButtonValue = true,
                FillWeight = 30
            });

            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Статус",
                FillWeight = 80,
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
                using (var conn = new NpgsqlConnection(DBConnection.connectionString_))
                {
                    conn.Open();
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
            }
            catch (Exception)
            {

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
            _model.LoadRecentRequests(15);
            DisplayRequests(_model.Requests);
        }

        private void DisplayRequests(List<RepairRequest> requests)
        {
            dgvRequests.Rows.Clear();

            if (requests == null)
                return;

            foreach (var req in requests)
            {
                string statusDisplay = req.Status == RepairStatus.в_работе
                    ? "в работе"
                    : req.Status.ToString();

                string techDisplay = string.IsNullOrWhiteSpace(req.TechnicianLogin)
                    ? "Не назначен"
                    : req.TechnicianLogin;

                int rowIndex = dgvRequests.Rows.Add(
                    req.Id,
                    req.CarNumber,
                    req.Description,
                    "+",
                    statusDisplay,
                    req.CreatedAt.ToString("dd.MM.yyyy HH:mm"),
                    techDisplay
                );

                dgvRequests.Rows[rowIndex].Tag = req;
            }
        }

        private void dgvRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvRequests.Columns[e.ColumnIndex].Name == "colMore")
            {
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
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку в таблице для назначения техника.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvRequests.SelectedRows[0];
            var request = selectedRow.Tag as RepairRequest;

            if (request == null)
                return;

            string selectedTechnician = cmbAssignTech.SelectedItem?.ToString();
            string errorMessage;

            bool success = _model.AssignTechnicianToRequest(request.Id, selectedTechnician, out errorMessage);

            if (success)
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

            var selectedRow = dgvRequests.SelectedRows[0];
            var request = selectedRow.Tag as RepairRequest;

            if (request == null)
                return;

            if (_currentUser != null && _currentUser.Role == Role.техник)
            {
                if (request.TechnicianLogin != _currentUser.Login)
                {
                    MessageBox.Show("Вы можете отменять только те заявки, которые назначены на вас.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string errorMessage;
            bool success = _model.CancelRequest(request.Id, out errorMessage);

            if (success)
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