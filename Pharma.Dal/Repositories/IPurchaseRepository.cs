using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Dal.Repositories
{
    public interface IPurchaseRepository
    {
        List<Purchase> GetAllPurchases();
        Purchase GetPurchaseById(int purchaseId);
        Supplier GetSupplierById (int supplierId);
        Item GetItemById (int itemId);

        void AddPurchase(Purchase purchase);
        void AddPurchaseItem (PurchaseItem purchaseItem);
        void SaveChanges();
        bool SupplierSuppliesItem(int supplierId, int itemId);
        Employee GetEmployeeById(int employeeId);
    }
}
