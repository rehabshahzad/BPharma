using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pharma.Dal.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly PharmacyDbContext _context;
        public PurchaseRepository(PharmacyDbContext context)
        {
            _context = context;
        }
        public List<Purchase> GetAllPurchases() {
            return _context.Purchases.AsNoTracking().ToList();
        }
        public Purchase GetPurchaseById(int id)
        {
            return _context.Purchases.Include(p => p.PurchaseItems).FirstOrDefault(p => p.PurchaseId == id);
        }
        public Supplier GetSupplierById(int id) {
            return _context.Suppliers.FirstOrDefault(s => s.SupplierId == id); }

        public Item GetItemById(int id) {
            return _context.Items.FirstOrDefault(i => i.ItemId == id);
        }
        public void AddPurchase(Purchase purchase) {
            _context.Purchases.Add(purchase);
        }
        public void AddPurchaseItem(PurchaseItem purchaseItem) {
            _context.PurchaseItems.Add(purchaseItem);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public bool SupplierSuppliesItem(int supplierId, int itemId) //validates if the supplier supplies those items
        {
            return _context.SupplierItems.Any(si =>
                si.SupplierId == supplierId &&
                si.ItemId == itemId &&
                si.IsActive);
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees
                .FirstOrDefault(e => e.EmployeeId == employeeId);
        }
    }
}
