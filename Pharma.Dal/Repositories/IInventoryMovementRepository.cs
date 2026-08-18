using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface IInventoryMovementRepository
    {
        List<InventoryMovement> GetAllMovements();

        InventoryMovement GetMovementById(int id);

        List<InventoryMovement> GetMovementsByBatchId(int batchId);

        Batch GetBatchById(int batchId);

        Employee GetEmployeeById(int employeeId);

        void AddMovement(InventoryMovement movement);

        void SaveChanges();
        int GetCurrentStockForBatch(int batchId);
    }
}