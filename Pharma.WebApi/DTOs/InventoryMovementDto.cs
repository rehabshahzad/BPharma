using Pharma.Entity.Enums;
using System;

namespace Pharma.BLL.DTOs
{
    public class InventoryMovementDto
    {
        public int InventoryMovementId { get; set; }

        public int BatchId { get; set; }

        public InventoryMovementType MovementType { get; set; }

        public int QuantityChange { get; set; }

        public int? ReferenceId { get; set; }

        public string Remarks { get; set; }

        public DateTime MovementDate { get; set; }

        public int PerformedByEmployeeId { get; set; }
       
    }
}