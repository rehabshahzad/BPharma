using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs
{
    public class PurchaseDto
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public PurchaseItemStatus Status {  get; set; }
        
        public decimal AdditionalCharges { get; set; }
        public string Notes { get; set; }

        public decimal SubtotalAmount { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<PurchaseItemDto> Items { get; set; }

    }
}