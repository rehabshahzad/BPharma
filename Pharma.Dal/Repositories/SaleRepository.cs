using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using Pharma.Entity.Enums;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly PharmacyDbContext _context;
        private DbContextTransaction _transaction;

        public SaleRepository(PharmacyDbContext context)
        {
            _context = context;
        }


        public List<Sale> GetAllSales()
        {
            return _context.Sales
                .Include(s => s.SaleItems)
                .AsNoTracking()
                .ToList();
        }


        public Sale GetSaleById(int id)
        {
            return _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefault(s => s.SaleId == id);
        }


        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers
                .FirstOrDefault(c =>
                    c.CustomerId == customerId);
        }


        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees
                .FirstOrDefault(e =>
                    e.EmployeeId == employeeId);
        }


        public Item GetItemById(int itemId)
        {
            return _context.Items
                .FirstOrDefault(i =>
                    i.ItemId == itemId);
        }


        public void AddSale(Sale sale)
        {
            _context.Sales.Add(sale);
        }


        public void AddSaleItem(SaleItem saleItem)
        {
            _context.SaleItems.Add(saleItem);
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public List<Batch> GetAvailableBatchesForItem(int itemId)
        {
            return _context.Batches
                .Where(b =>
                    b.PurchaseItem.ItemId == itemId &&
                    b.Status == BatchStatus.Available &&
                    b.ExpiryDate > DateTime.Now)
                .OrderBy(b => b.ExpiryDate)
                .ToList();
        }
        public int GetAllocatedQuantityForBatch(int batchId)
        {
            return _context.BatchAllocations
                .Where(ba => ba.BatchId == batchId)
                .Select(ba => (int?)ba.AllocatedQuantity)
                .Sum() ?? 0;
        }
        public void AddBatchAllocation(BatchAllocation allocation)
        {
            _context.BatchAllocations.Add(allocation);
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }


    }
   
    }
