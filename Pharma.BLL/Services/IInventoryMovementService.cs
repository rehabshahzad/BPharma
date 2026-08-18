using Pharma.Entity.Entities;
using Pharma.Entity.Enums;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface IInventoryMovementService
    {
        List<InventoryMovement> GetAllMovements();

        InventoryMovement GetMovementById(int id);

        List<InventoryMovement> GetMovementsByBatchId(int batchId);

        InventoryMovement CreateMovement(
            int batchId,
            InventoryMovementType movementType,
            int quantity,
            int? referenceId,
            string remarks,
            int employeeId);
    }
}