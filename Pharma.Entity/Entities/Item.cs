using PharmacyManagement.Entity.Entities;
using System;
using Pharma.Entity.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class Item
    {
        public int ItemId { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public int? FormulaId { get; set; }
        public virtual Formula Formula { get; set; }

        public string ItemName { get; set; }

        //Identifies whether the item is a medicine or general product.
        public ItemType ItemType { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public string Barcode { get; set; }

        //Indicates whether the medicine requires a prescription.
        public bool IsPrescriptionRequired { get; set; }

        public decimal SellingPrice { get; set; }

        public int MinimumStockLevel { get; set; }

        public string RackNumber { get; set; }

        public string ShelfNumber { get; set; }

        public string LaneNumber { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedByEmployeeId { get; set; }

        public virtual Employee UpdatedByEmployee { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
