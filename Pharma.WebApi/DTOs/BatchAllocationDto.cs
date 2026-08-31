using System;

namespace PharmacyMangementSystem.DTOs
{
    public class BatchAllocationDto
    {
        public int BatchAllocationId { get; set; }

        public int SaleItemId { get; set; }

        public int BatchId { get; set; }

        public int AllocatedQuantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}