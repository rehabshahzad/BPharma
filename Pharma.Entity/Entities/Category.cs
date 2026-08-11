using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public bool isActive { get; set; }

        public int CreatedByEmployeeId { get; set; }
        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedByEmployeeId { get; set; }

        public virtual Employee UpdatedByEmployee { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
