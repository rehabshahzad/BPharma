using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;

namespace PharmacyMangementSystem.DTOs.SupplierReturn
{
    public class SupplierReturnDto
    {
        public int SupplierReturnId { get; set; }

        public int PurchaseId { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Reason { get; set; }

        public SupplierReturnStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<SupplierReturnItemDto> Items { get; set; }
    }
}