using System;

namespace ControlStateCar
{
    public class RepairRequestDetails
    {
        public int Id { get; set; }
        public RepairStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarNumber { get; set; }
        public string Vin { get; set; }
        public int Year { get; set; }

        public string TechnicianLogin { get; set; }
        public string TechnicianFullName { get; set; }
        public string TechnicianPhone { get; set; }

        public string Description { get; set; }
    }
}