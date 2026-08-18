using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface IBatchRepository
    {
        List<Batch> GetAllBatches();

        Batch GetBatchById(int id);

        PurchaseItem GetPurchaseItemById(int purchaseItemId);

        Employee GetEmployeeById(int employeeId);

        bool BatchNumberExists(
            string batchNumber,
            int? excludeBatchId = null
        );
        int GetTotalReceivedQuantity(int purchaseItemId);
        void AddBatch(Batch batch);

        void SaveChanges();
        void AddInventoryMovement(InventoryMovement movement);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        List<Batch> GetExpiredAvailableBatches();

        int GetCurrentStockForBatch(int batchId);

        
    }
}