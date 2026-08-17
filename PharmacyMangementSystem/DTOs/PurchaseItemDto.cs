using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs
{
    public class PurchaseItemDto
    {
        public int PurchaseItemId { get; set; }

        public int ItemId { get; set; }

        public int OrderedQuantity { get; set; }

        public decimal UnitPurchasePrice { get; set; }
    }
}