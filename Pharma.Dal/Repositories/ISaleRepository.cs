using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace Pharma.Dal.Repositories
{
    public interface ISaleRepository
    {
        List<Sale> GetAllSales();

        Sale GetSaleById(int id);

        Customer GetCustomerById(int customerId);

        Employee GetEmployeeById(int employeeId);

        Item GetItemById(int itemId);

        void AddSale(Sale sale);

        void AddSaleItem(SaleItem saleItem);
        List<Batch> GetAvailableBatchesForItem(int itemId);

        int GetAllocatedQuantityForBatch(int batchId);

        void AddBatchAllocation(BatchAllocation allocation);

        void SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}