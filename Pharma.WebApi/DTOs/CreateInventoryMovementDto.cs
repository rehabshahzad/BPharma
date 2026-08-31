using Pharma.Entity.Enums;

namespace Pharma.BLL.DTOs
{
    public class CreateInventoryMovementDto
    {
        public int BatchId { get; set; }

        public InventoryMovementType MovementType { get; set; }

        public int Quantity { get; set; }

        public string Remarks { get; set; }
    }
}