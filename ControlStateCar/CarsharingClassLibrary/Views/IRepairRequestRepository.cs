using System.Collections.Generic;
using CarsharingClassLibrary.Models;

namespace ControlStateCar
{
    public interface IRepairRequestRepository
    {
        List<RepairRequest> GetAllRepairRequests();
        bool AssignTechnician(int requestId, string technicianLogin);
        bool CancelRequest(int requestId, string technicianLogin);
    }
}