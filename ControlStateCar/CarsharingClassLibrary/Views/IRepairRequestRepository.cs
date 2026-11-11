using System.Collections.Generic;
using CarsharingClassLibrary.Models;

namespace ControlStateCar
{
    public interface IRepairRequestRepository
    {
        List<RepairRequest> GetRecentRepairRequests(Role role, string userLogin, int count = 15);
        List<RepairRequest> FilterRepairRequests(Role userRole, string currentUserLogin, RepairRequestFilterParameters filterParams);
        bool AssignTechnician(int requestId, string technicianLogin);
        bool CancelRequest(int requestId, string technicianLogin);
    }
}