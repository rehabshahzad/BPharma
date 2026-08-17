using Org.BouncyCastle.Bcpg.OpenPgp;
using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public SaleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Notes { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }
}