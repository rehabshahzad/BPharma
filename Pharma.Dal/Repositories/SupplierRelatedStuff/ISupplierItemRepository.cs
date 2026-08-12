using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface ISupplierItemRepository
    {
        List<SupplierItem> GetAllSupplierItems();

        SupplierItem GetSupplierItemById(int id);

        void Add(SupplierItem supplierItem);

        bool SupplierItemExists(
            int supplierId,
            int itemId,
            int? excludeSupplierItemId = null
        );

        bool SupplierExists(int supplierId);

        bool ItemExists(int itemId);

        void SaveChanges();
    }
}