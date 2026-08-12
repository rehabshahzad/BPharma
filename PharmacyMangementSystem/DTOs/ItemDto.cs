using System;

namespace PharmacyMangementSystem.DTOs.Item
{
    public class ItemDto
    {
        public int ItemId { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int? FormulaId { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public string Barcode { get; set; }

        public bool IsPrescriptionRequired { get; set; }

        public decimal SellingPrice { get; set; }

        public int MinimumStockLevel { get; set; }

        public string RackNumber { get; set; }

        public string ShelfNumber { get; set; }

        public string LaneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}