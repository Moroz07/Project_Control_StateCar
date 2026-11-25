using System;
using System.Collections.Generic;
using CarsharingClassLibrary.Models;

namespace ControlStateCar
{
    public interface IRepairRequestRepository
    {
        List<RepairRequest> GetAllRepairRequests();
        List<RepairRequestDetails> GetAllRepairRequestDetails();
        List<string> GetAllTechnicianLogins();
        bool AssignTechnician(int requestId, string technicianLogin);
        bool CancelRequest(int requestId, string technicianLogin);
        bool CloseRequest(int requestId, string workDone, DateTime closedAt);
        bool UpdateClosedDate(int requestId, DateTime closedAt, string workDone);
        List<RepairRequestHistoryItem> GetAllRequestHistory();
        bool AddRequestHistory(int requestId, int? employeeId, string statusLabel, string comment, DateTime changedAt);
    }
}