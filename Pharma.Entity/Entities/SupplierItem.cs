using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class SupplierItem
    {
        public int SupplierItemId { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public decimal SupplierPrice { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByEmployeeId { get; set; }
        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedByEmployeeId { get; set; }
        public virtual Employee UpdatedByEmployee { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
