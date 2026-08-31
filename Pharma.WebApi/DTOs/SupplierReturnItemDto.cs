using System;

namespace PharmacyMangementSystem.DTOs.SupplierReturn
{
    public class SupplierReturnItemDto
    {
        public int SupplierReturnItemId { get; set; }

        public int BatchId { get; set; }

        public int ReturnQuantity { get; set; }

        public decimal ReturnAmount { get; set; }

        public string Reason { get; set; }
    }
}