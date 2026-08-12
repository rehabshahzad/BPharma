using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface ISupplierReturnRepository
    {
        List<SupplierReturn> GetAllSupplierReturns();

        SupplierReturn GetSupplierReturnById(int id);

        Purchase GetPurchaseById(int purchaseId);

        Batch GetBatchById(int batchId);

        bool BatchBelongsToPurchase(
            int batchId,
            int purchaseId
        );

        void AddReturn(SupplierReturn supplierReturn);

        void AddReturnItem(SupplierReturnItem item);

        void SaveChanges();
    }
}