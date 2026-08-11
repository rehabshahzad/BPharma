using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; } //the company name
        public string ContactPersonName { get; set; } //person in contact
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedByEmployeeId { get; set; }

        public virtual Employee UpdatedByEmployee { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
