using System;
using System.Collections.Generic;
using CarsharingClassLibrary.Models;

namespace ControlStateCar
{
    public class RepairRequestsListModel
    {
        private IRepairRequestRepository repository_;
        private User currentUser_;

        public List<RepairRequest> Requests { get; set; }

        public int TotalCount
        {
            get
            {
                if (Requests == null)
                {
                    return 0;
                }
                return Requests.Count;
            }
        }

        public RepairRequestsListModel(IRepairRequestRepository repository, User currentUser = null)
        {
            repository_ = repository;
            currentUser_ = currentUser;
            Requests = new List<RepairRequest>();
        }

        private int CompareByCreatedAtDesc(RepairRequest a, RepairRequest b)
        {
            return b.CreatedAt.CompareTo(a.CreatedAt);
        }

        public void LoadRecentRequests(int count = 15)
        {
            var allRequests = repository_.GetAllRepairRequests();
            if (allRequests == null)
            {
                Requests = new List<RepairRequest>();
                return;
            }

            // Сортировка по дате создания (сначала новые) на уровне программы
            allRequests.Sort(CompareByCreatedAtDesc);

            // Ограничение выборки до count (15) записей на уровне программы
            var recent = new List<RepairRequest>();
            int limit = count;
            if (allRequests.Count < limit)
            {
                limit = allRequests.Count;
            }

            for (int i = 0; i < limit; i++)
            {
                recent.Add(allRequests[i]);
            }

            Requests = recent;
        }

        public string ApplyFilter(RepairRequestFilterParameters filterParams)
        {
            var allRequests = repository_.GetAllRepairRequests();
            if (allRequests == null)
            {
                Requests = new List<RepairRequest>();
                return "Заявки по заданным критериям не найдены";
            }

            if (filterParams == null)
            {
                allRequests.Sort(CompareByCreatedAtDesc);
                Requests = allRequests;
                if (Requests.Count == 0)
                {
                    return "Заявки по заданным критериям не найдены";
                }
                return string.Empty;
            }

            var filtered = new List<RepairRequest>();

            foreach (var req in allRequests)
            {
                bool matches = true;

                // Фильтрация по госномеру (без учёта регистра, по подстроке)
                if (filterParams.CarNumber != null && filterParams.CarNumber.Trim() != "")
                {
                    string searchCar = filterParams.CarNumber.Trim().ToLower();
                    if (req.CarNumber == null || !req.CarNumber.ToLower().Contains(searchCar))
                    {
                        matches = false;
                    }
                }

                // Фильтрация по статусу
                if (matches && filterParams.Status.HasValue)
                {
                    if (req.Status != filterParams.Status.Value)
                    {
                        matches = false;
                    }
                }

                // Фильтрация по логину техника (доступна всем пользователям)
                if (matches && filterParams.TechnicianLogin != null && filterParams.TechnicianLogin.Trim() != "")
                {
                    string searchTech = filterParams.TechnicianLogin.Trim().ToLower();
                    if (req.TechnicianLogin == null || !req.TechnicianLogin.ToLower().Contains(searchTech))
                    {
                        matches = false;
                    }
                }

                // Фильтрация по календарной дате создания
                if (matches && filterParams.CreatedDate.HasValue)
                {
                    if (req.CreatedAt.Date != filterParams.CreatedDate.Value.Date)
                    {
                        matches = false;
                    }
                }

                if (matches)
                {
                    filtered.Add(req);
                }
            }

            // Сортировка отфильтрованного списка (сначала новые) на уровне программы
            filtered.Sort(CompareByCreatedAtDesc);
            Requests = filtered;

            if (Requests.Count == 0)
            {
                return "Заявки по заданным критериям не найдены";
            }

            return string.Empty;
        }

        public string AssignTechnicianToRequest(int requestId, string technicianLogin)
        {
            if (technicianLogin == null || technicianLogin.Trim() == "" || technicianLogin == "Нет техников")
            {
                return "Не выбран техник для назначения.";
            }

            bool success = repository_.AssignTechnician(requestId, technicianLogin);
            if (success)
            {
                return string.Empty;
            }
            else
            {
                return "Не удалось назначить техника на заявку.";
            }
        }

        public string CancelRequest(int requestId)
        {
            string techLogin = "";
            if (currentUser_ != null)
            {
                techLogin = currentUser_.Login;
            }

            bool success = repository_.CancelRequest(requestId, techLogin);
            if (success)
            {
                return string.Empty;
            }
            else
            {
                return "Не удалось отменить заявку.";
            }
        }

        public RepairRequestDetails GetRequestDetails(int requestId)
        {
            if (requestId <= 0)
            {
                return null;
            }

            var allDetails = repository_.GetAllRepairRequestDetails();
            if (allDetails == null)
            {
                return null;
            }

            foreach (var item in allDetails)
            {
                if (item.Id == requestId)
                {
                    return item;
                }
            }

            return null;
        }

        public List<string> GetTechnicianLogins()
        {
            var logins = repository_.GetAllTechnicianLogins();
            if (logins == null)
            {
                return new List<string>();
            }

            logins.Sort(StringComparer.OrdinalIgnoreCase);
            return logins;
        }

        public List<RepairRequestGridItem> GetRequestsForGrid()
        {
            var gridItems = new List<RepairRequestGridItem>();
            if (Requests == null)
            {
                return gridItems;
            }

            foreach (var req in Requests)
            {
                string statusText;
                switch (req.Status)
                {
                    case RepairStatus.New:
                        statusText = "новый";
                        break;
                    case RepairStatus.InProgress:
                        statusText = "в работе";
                        break;
                    case RepairStatus.Completed:
                        statusText = "завершён";
                        break;
                    case RepairStatus.Cancelled:
                        statusText = "отменена";
                        break;
                    default:
                        statusText = "новый";
                        break;
                }

                string techText = string.IsNullOrWhiteSpace(req.TechnicianLogin)
                    ? "Не назначен"
                    : req.TechnicianLogin;

                gridItems.Add(new RepairRequestGridItem
                {
                    Id = req.Id,
                    CarNumber = req.CarNumber,
                    Description = req.Description,
                    StatusDisplay = statusText,
                    CreatedAtDisplay = req.CreatedAt.ToString("dd.MM.yyyy HH:mm"),
                    TechnicianDisplay = techText,
                    TechnicianLogin = req.TechnicianLogin
                });
            }

            return gridItems;
        }

        public string CloseRequest(int requestId, string workDone, DateTime closedAt)
        {
            if (requestId <= 0)
            {
                return "Некорректный идентификатор заявки.";
            }

            if (string.IsNullOrWhiteSpace(workDone))
            {
                return "Укажите описание выполненных работ перед закрытием заявки.";
            }

            var details = GetRequestDetails(requestId);
            if (details == null)
            {
                return "Заявка не найдена.";
            }

            if (details.Status == RepairStatus.Cancelled)
            {
                return "Невозможно закрыть отменённую заявку.";
            }

            if (closedAt < details.CreatedAt)
            {
                return "Дата закрытия не может быть раньше даты создания заявки.";
            }

            bool updated = repository_.CloseRequest(requestId, workDone.Trim(), closedAt);
            return updated ? string.Empty : "Не удалось закрыть заявку в базе данных.";
        }

        public string UpdateRequestClosedDate(int requestId, DateTime newClosedAt, string workDone)
        {
            if (requestId <= 0)
            {
                return "Некорректный идентификатор заявки.";
            }

            var details = GetRequestDetails(requestId);
            if (details == null)
            {
                return "Заявка не найдена.";
            }

            if (newClosedAt < details.CreatedAt)
            {
                return "Дата закрытия не может быть раньше даты создания заявки.";
            }

            bool updated = repository_.UpdateClosedDate(requestId, newClosedAt, (workDone ?? "").Trim());
            return updated ? string.Empty : "Не удалось обновить дату закрытия заявки.";
        }
    }
}