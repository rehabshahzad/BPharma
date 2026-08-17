using Pharma.Entity.Enums;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pharma.Entity.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal SubtotalAmount { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal TotalAmount { get; set; }

        public string Notes { get; set; }
        public PurchaseItemStatus Status { get; set; }

        public int CreatedByEmployeeId { get; set; }
        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedByEmployeeId { get; set; }
        public virtual Employee UpdatedByEmployee { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
