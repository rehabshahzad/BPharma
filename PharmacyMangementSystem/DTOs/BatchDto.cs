using Pharma.Entity.Enums;
using System;

namespace PharmacyMangementSystem.DTOs
{
    public class BatchDto
    {
        public int BatchId { get; set; }

        public int PurchaseItemId { get; set; }

        public BatchStatus Status { get; set; }

        public string BatchNumber { get; set; }

        public int ReceivedQuantity { get; set; }

        public DateTime? ManufacturingDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ReceivedDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}