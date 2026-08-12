using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class CustomerReturnRepository
        : ICustomerReturnRepository
    {
        private readonly PharmacyDbContext _context;

        public CustomerReturnRepository(
            PharmacyDbContext context)
        {
            _context = context;
        }


        public List<CustomerReturn> GetAllCustomerReturns()
        {
            return _context.CustomerReturns
                .ToList();
        }


        public CustomerReturn GetCustomerReturnById(
            int id)
        {
            return _context.CustomerReturns
                .FirstOrDefault(cr =>
                    cr.CustomerReturnId == id
                );
        }


        public Sale GetSaleById(
            int saleId)
        {
            return _context.Sales
                .FirstOrDefault(s =>
                    s.SaleId == saleId
                );
        }


        public SaleItem GetSaleItemById(
            int saleItemId)
        {
            return _context.SaleItems
                .FirstOrDefault(si =>
                    si.SaleItemId == saleItemId
                );
        }


        public Batch GetBatchById(
            int batchId)
        {
            return _context.Batches
                .FirstOrDefault(b =>
                    b.BatchId == batchId
                );
        }


        public BatchAllocation GetBatchAllocation(
            int batchId,
            int saleItemId)
        {
            return _context.BatchAllocations
                .FirstOrDefault(ba =>
                    ba.BatchId == batchId &&
                    ba.SaleItemId == saleItemId
                );
        }


        public bool SaleItemBelongsToSale(
            int saleItemId,
            int saleId)
        {
            return _context.SaleItems
                .Any(si =>
                    si.SaleItemId == saleItemId &&
                    si.SaleId == saleId
                );
        }


        public bool BatchWasUsedForSaleItem(
            int batchId,
            int saleItemId)
        {
            return _context.BatchAllocations
                .Any(ba =>
                    ba.BatchId == batchId &&
                    ba.SaleItemId == saleItemId
                );
        }


        public int GetAlreadyReturnedQuantity(
            int saleItemId,
            int batchId)
        {
            return _context.CustomerReturnItems
                .Where(cri =>
                    cri.SaleItemId == saleItemId &&
                    cri.BatchId == batchId
                )
                .Select(cri =>
                    (int?)cri.ReturnQuantity
                )
                .Sum() ?? 0;
        }


        public void AddReturn(
            CustomerReturn customerReturn)
        {
            _context.CustomerReturns
                .Add(customerReturn);
        }


        public void AddReturnItem(
            CustomerReturnItem item)
        {
            _context.CustomerReturnItems
                .Add(item);
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}