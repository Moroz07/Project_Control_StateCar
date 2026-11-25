using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsharingClassLibrary.Models
{
    public class RepairRequestHistoryItem
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public DateTime ChangedAt { get; set; }
        public string StatusName { get; set; }
        public string EmployeeDisplay { get; set; }
        public string ServiceTypeName { get; set; }
        public string Comment { get; set; }
    }
}
