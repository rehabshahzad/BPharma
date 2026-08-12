using System;

namespace PharmacyMangementSystem.DTOs.SupplierItem
{
    public class SupplierItemDto
    {
        public int SupplierItemId { get; set; }

        public int SupplierId { get; set; }

        public int ItemId { get; set; }

        public decimal SupplierPrice { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}