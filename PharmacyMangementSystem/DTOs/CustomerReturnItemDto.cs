namespace PharmacyMangementSystem.DTOs.CustomerReturn
{
    public class CustomerReturnItemDto
    {
        public int CustomerReturnItemId { get; set; }

        public int SaleItemId { get; set; }

        public int BatchId { get; set; }

        public int ReturnQuantity { get; set; }

        public decimal RefundAmount { get; set; } // response only logically

        public string Reason { get; set; }

        public bool CanReturnToStock { get; set; }
    }
}