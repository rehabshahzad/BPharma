using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly PharmacyDbContext _context;


        public SupplierRepository(
            PharmacyDbContext context)
        {
            _context = context;
        }


        public Supplier GetSupplierById(int id)
        {
            return _context.Suppliers
                .FirstOrDefault(
                    s => s.SupplierId == id
                );
        }


        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers
                .AsNoTracking()
                .ToList();
        }


        public void AddSupplier(
            Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
        }


        public bool SupplierExists(
            string supplierName,
            string email,
            int? excludeSupplierId = null)
        {
            return _context.Suppliers.Any(s =>
                s.SupplierName == supplierName &&
                s.Email == email &&
                (
                    !excludeSupplierId.HasValue ||
                    s.SupplierId != excludeSupplierId.Value
                )
            );
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}