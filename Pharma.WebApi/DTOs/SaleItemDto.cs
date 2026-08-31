using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs
{
    public class SaleItemDto
    {
        public int SaleItemId { get; set; }
        public int OrderedQuantity { get; set; }
        public int ItemId { get; set; }
        public decimal UnitSalePrice { get; set; }
   

    }
}