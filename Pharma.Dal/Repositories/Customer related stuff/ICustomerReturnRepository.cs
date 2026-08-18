using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface ICustomerReturnRepository
    {
        List<CustomerReturn> GetAllCustomerReturns();

        CustomerReturn GetCustomerReturnById(int id);

        Sale GetSaleById(int saleId);

        SaleItem GetSaleItemById(int saleItemId);

        Batch GetBatchById(int batchId);

        BatchAllocation GetBatchAllocation(
            int batchId,
            int saleItemId
        );

        bool SaleItemBelongsToSale(
            int saleItemId,
            int saleId
        );

        bool BatchWasUsedForSaleItem(
            int batchId,
            int saleItemId
        );

        int GetAlreadyReturnedQuantity(
            int saleItemId,
            int batchId
        );

        void AddReturn(
            CustomerReturn customerReturn
        );

        void AddReturnItem(
            CustomerReturnItem item
        );

        void SaveChanges();
        void AddInventoryMovement(InventoryMovement movement);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}