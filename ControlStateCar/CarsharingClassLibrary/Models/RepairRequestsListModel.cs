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

        public void LoadRecentRequests(int count = 15)
        {
            Role role = Role.администратор;
            string login = "";

            if (currentUser_ != null)
            {
                role = currentUser_.Role;
                login = currentUser_.Login;
            }

            Requests = repository_.GetRecentRepairRequests(role, login, count);
        }

        public string ApplyFilter(RepairRequestFilterParameters filterParams)
        {
            Role role = Role.администратор;
            string login = "";

            if (currentUser_ != null)
            {
                role = currentUser_.Role;
                login = currentUser_.Login;
            }

            if (role == Role.техник)
            {
                if (filterParams != null)
                {
                    filterParams.TechnicianLogin = login;
                }
            }

            var results = repository_.FilterRepairRequests(role, login, filterParams);
            Requests = results;

            if (results == null || results.Count == 0)
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
    }
}