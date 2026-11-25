using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsharingClassLibrary.Models
{
    public class RepairRequestGridItem
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string Description { get; set; }
        public string StatusDisplay { get; set; }
        public string CreatedAtDisplay { get; set; }
        public string TechnicianDisplay { get; set; }
        public string TechnicianLogin { get; set; }
    }
}
