using System;

namespace ControlStateCar
{
    public enum RepairStatus
    {
        новый,
        в_работе,
        завершён,
        отменена
    }

    public class RepairRequest
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public string Description { get; set; }
        public RepairStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TechnicianLogin { get; set; }
    }
}