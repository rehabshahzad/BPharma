using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface ISupplierService
    {
        Supplier GetSupplierById(int id);

        List<Supplier> GetAllSuppliers();

        Supplier CreateSupplier(
            Supplier supplier,
            int createdByEmployeeId
        );

        Supplier UpdateSupplier(
            int id,
            Supplier supplier,
            int updatedByEmployeeId
        );
    }
}