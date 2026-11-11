using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlStateCar;

namespace CarsharingClassLibrary.Models
{
    public class RepairRequestFilterParameters
    {
        public string CarNumber { get; set; }
        public RepairStatus? Status { get; set; }
        public string TechnicianLogin { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
