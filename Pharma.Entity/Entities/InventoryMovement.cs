using Pharma.Entity.Enums;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class InventoryMovement
    {
        public int InventoryMovementId { get; set; }

        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }

        public InventoryMovementType MovementType { get; set; }

        //Positive for stock-in and negative for stock-out.
        public int QuantityChange { get; set; }

        public int? ReferenceId { get; set; } //stock changed bec of which transaction
        //ref id is optional cuz manual adjustments dont have sale or purchase ids

        public string Remarks { get; set; }

        public DateTime MovementDate { get; set; }

        public int PerformedByEmployeeId { get; set; }
        public virtual Employee PerformedByEmployee { get; set; } 
    }
}
