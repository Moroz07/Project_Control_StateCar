using System.Collections.Generic;

namespace ControlStateCar
{
    public class RepairRequestsListModel
    {
        private IRepairRequestRepository _repository;
        private User _currentUser;

        public List<RepairRequest> Requests { get; set; }
        public int TotalCount => Requests?.Count ?? 0;

        public RepairRequestsListModel(IRepairRequestRepository repository, User currentUser = null)
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
                Requests = _repository.GetRecentRepairRequests(Role.администратор, string.Empty, count);
            }
        }

        public string AssignTechnicianToRequest(int requestId, string technicianLogin)
        {
            if (string.IsNullOrEmpty(technicianLogin))
            {
                return "Выберите техника из списка.";
            }

            bool isSuccess = _repository.AssignTechnician(requestId, technicianLogin);
            if (isSuccess)
            {
                return string.Empty;
            }

            return "Не удалось назначить техника в базе данных.";
        }

        public string CancelRequest(int requestId)
        {
            if (_currentUser == null || string.IsNullOrEmpty(_currentUser.Login))
            {
                return "Ошибка определения текущего пользователя.";
            }

            bool isSuccess = _repository.CancelRequest(requestId, _currentUser.Login);
            if (isSuccess)
            {
                return string.Empty;
            }

            return "Не удалось отменить заявку. Убедитесь, что она назначена на вас.";
        }
    }
}