using System.Collections.Generic;

namespace ControlStateCar
{
    public interface IRepairRequestRepository
    {
        List<RepairRequest> GetRecentRepairRequests(Role role, string userLogin, int count = 15);
        bool AssignTechnician(int requestId, string technicianLogin);
        bool CancelRequest(int requestId, string technicianLogin);
    }
}