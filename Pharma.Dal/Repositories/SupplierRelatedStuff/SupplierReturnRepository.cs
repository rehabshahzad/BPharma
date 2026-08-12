using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class SupplierReturnRepository
        : ISupplierReturnRepository
    {
        private readonly PharmacyDbContext _context;

        public SupplierReturnRepository(
            PharmacyDbContext context)
        {
            _context = context;
        }


        public List<SupplierReturn>
            GetAllSupplierReturns()
        {
            return _context.SupplierReturns
                .ToList();
        }


        public SupplierReturn
            GetSupplierReturnById(int id)
        {
            return _context.SupplierReturns
                .FirstOrDefault(sr =>
                    sr.SupplierReturnId == id
                );
        }


        public Purchase GetPurchaseById(
            int purchaseId)
        {
            return _context.Purchases
                .FirstOrDefault(p =>
                    p.PurchaseId == purchaseId
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


        public bool BatchBelongsToPurchase(
            int batchId,
            int purchaseId)
        {
            return _context.Batches.Any(b =>
                b.BatchId == batchId &&
                b.PurchaseItem.PurchaseId == purchaseId
            );
        }


        public void AddReturn(
            SupplierReturn supplierReturn)
        {
            _context.SupplierReturns
                .Add(supplierReturn);
        }


        public void AddReturnItem(
            SupplierReturnItem item)
        {
            _context.SupplierReturnItems
                .Add(item);
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}