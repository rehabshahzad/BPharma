using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface ISupplierRepository
    {
        Supplier GetSupplierById(int id);

        List<Supplier> GetAllSuppliers();

        void AddSupplier(Supplier supplier);

        bool SupplierExists(
            string supplierName,
            string email,
            int? excludeSupplierId = null
        );

        void SaveChanges();
    }
}