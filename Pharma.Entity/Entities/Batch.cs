using Pharma.Entity.Enums;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class Batch
    {
        public int BatchId { get; set; }

        public int PurchaseItemId { get; set; }
        public virtual PurchaseItem PurchaseItem { get; set; }
        public BatchStatus Status { get; set; }

        public string BatchNumber { get; set; }

        public int ReceivedQuantity { get; set; }

        public DateTime? ManufacturingDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ReceivedDate { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Employee UpdatedByEmployee { get;set; }
        public int? UpdatedByEmployeeId {  get; set; }

        public virtual ICollection<BatchAllocation> BatchAllocations { get; set; }
    }
}
