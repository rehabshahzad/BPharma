using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface ISupplierReturnService
    {
        List<SupplierReturn> GetAllSupplierReturns();

        SupplierReturn GetSupplierReturnById(int id);

        SupplierReturn CreateSupplierReturn(
            SupplierReturn supplierReturn,
            List<SupplierReturnItem> items,
            int employeeId
        );

        SupplierReturn UpdateSupplierReturn(
            int id,
            SupplierReturn supplierReturn,
            int employeeId
        );
    }
}