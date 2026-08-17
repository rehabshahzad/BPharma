using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly PharmacyDbContext _context;

        public BatchRepository(PharmacyDbContext context)
        {
            _context = context;
        }


        public List<Batch> GetAllBatches()
        {
            return _context.Batches
                .Include(b => b.PurchaseItem)
                .AsNoTracking()
                .ToList();
        }


        public Batch GetBatchById(int id)
        {
            return _context.Batches
                .Include(b => b.PurchaseItem)
                .FirstOrDefault(b => b.BatchId == id);
        }


        public PurchaseItem GetPurchaseItemById(
            int purchaseItemId)
        {
            return _context.PurchaseItems
                .FirstOrDefault(pi =>
                    pi.PurchaseItemId == purchaseItemId
                );
        }


        public Employee GetEmployeeById(
            int employeeId)
        {
            return _context.Employees
                .FirstOrDefault(e =>
                    e.EmployeeId == employeeId
                );
        }


        public bool BatchNumberExists(
            string batchNumber,
            int? excludeBatchId = null)
        {
            return _context.Batches.Any(b =>
                b.BatchNumber == batchNumber &&
                (!excludeBatchId.HasValue ||
                 b.BatchId != excludeBatchId.Value)
            );
        }


        public void AddBatch(Batch batch)
        {
            _context.Batches.Add(batch);
        }
        public int GetTotalReceivedQuantity(
    int purchaseItemId)
        {
            return _context.Batches
                .Where(b =>
                    b.PurchaseItemId == purchaseItemId
                )
                .Select(b =>
                    (int?)b.ReceivedQuantity
                )
                .Sum() ?? 0;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}