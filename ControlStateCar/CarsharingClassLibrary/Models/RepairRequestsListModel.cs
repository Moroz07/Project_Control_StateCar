using System.Collections.Generic;

namespace ControlStateCar
{
    public class RepairRequestsListModel
    {
        private IRepairRequestRepository _repository;
        private User _currentUser;

        public List<RepairRequest> Requests { get; set; }
        public int TotalCount => Requests?.Count ?? 0;

        public RepairRequestsListModel(IRepairRequestRepository repository, User currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
            Requests = new List<RepairRequest>();
        }

        public void LoadRecentRequests(int count = 15)
        {
            if (_currentUser != null)
            {
                Requests = _repository.GetRecentRepairRequests(_currentUser.Role, _currentUser.Login, count);
            }
            else
            {
                Requests = new List<RepairRequest>();
            }
        }

        public bool AssignTechnicianToRequest(int requestId, string technicianLogin, out string errorMessage)
        {
            if (string.IsNullOrEmpty(technicianLogin))
            {
                errorMessage = "Выберите техника из списка.";
                return false;
            }

            bool isSuccess = _repository.AssignTechnician(requestId, technicianLogin);
            if (isSuccess)
            {
                errorMessage = "";
                return true;
            }
            else
            {
                errorMessage = "Не удалось назначить техника в базе данных.";
                return false;
            }
        }
    }
}