using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface IPurchaseService
    {
        List<Purchase> GetAllPurchases();

        Purchase GetPurchaseById(int id);

        Purchase CreatePurchase(
            Purchase purchase,
            List<PurchaseItem> items,
            int employeeId
        );

        Purchase UpdatePurchase(
            int id,
            Purchase purchase,
            int employeeId
        );
    }
}