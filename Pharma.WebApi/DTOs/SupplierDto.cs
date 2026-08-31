using System;

namespace PharmacyMangementSystem.DTOs.Supplier
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string ContactPersonName { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}