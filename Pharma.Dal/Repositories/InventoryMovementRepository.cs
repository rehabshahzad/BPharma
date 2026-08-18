using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class InventoryMovementRepository
        : IInventoryMovementRepository
    {
        private readonly PharmacyDbContext _context;

        public InventoryMovementRepository(
            PharmacyDbContext context)
        {
            _context = context;
        }


        public List<InventoryMovement> GetAllMovements()
        {
            return _context.InventoryMovements
                .Include(im => im.Batch)
                .Include(im => im.PerformedByEmployee)
                .AsNoTracking()
                .ToList();
        }


        public InventoryMovement GetMovementById(int id)
        {
            return _context.InventoryMovements
                .Include(im => im.Batch)
                .Include(im => im.PerformedByEmployee)
                .FirstOrDefault(
                    im => im.InventoryMovementId == id
                );
        }


        public List<InventoryMovement> GetMovementsByBatchId(
            int batchId)
        {
            return _context.InventoryMovements
                .Where(im => im.BatchId == batchId)
                .OrderBy(im => im.MovementDate)
                .AsNoTracking()
                .ToList();
        }


        public Batch GetBatchById(int batchId)
        {
            return _context.Batches
                .FirstOrDefault(
                    b => b.BatchId == batchId
                );
        }


        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees
                .FirstOrDefault(
                    e => e.EmployeeId == employeeId
                );
        }


        public void AddMovement(
            InventoryMovement movement)
        {
            _context.InventoryMovements.Add(
                movement
            );
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public int GetCurrentStockForBatch(int batchId)
        {
            return _context.InventoryMovements
                .Where(im => im.BatchId == batchId)
                .Select(im => (int?)im.QuantityChange)
                .Sum() ?? 0;
        }
    }
}