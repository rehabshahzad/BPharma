using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface ISupplierItemService
    {
        List<SupplierItem>
            GetAllSupplierItems();

        SupplierItem
            GetSupplierItemById(int id);

        SupplierItem CreateSupplierItem(
            SupplierItem supplierItem,
            int createdByEmployeeId
        );

        SupplierItem UpdateSupplierItem(
            int id,
            SupplierItem supplierItem,
            int updatedByEmployeeId
        );
    }
}