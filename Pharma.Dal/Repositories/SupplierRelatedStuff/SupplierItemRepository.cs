using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class SupplierItemRepository
        : ISupplierItemRepository
    {
        private readonly PharmacyDbContext _context;


        public SupplierItemRepository(
            PharmacyDbContext context)
        {
            _context = context;
        }


        public List<SupplierItem>
            GetAllSupplierItems()
        {
            return _context.SupplierItems
                .ToList();
        }


        public SupplierItem
            GetSupplierItemById(int id)
        {
            return _context.SupplierItems
                .FirstOrDefault(si =>
                    si.SupplierItemId == id
                );
        }


        public void Add(
            SupplierItem supplierItem)
        {
            _context.SupplierItems
                .Add(supplierItem);
        }


        public bool SupplierItemExists(
            int supplierId,
            int itemId,
            int? excludeSupplierItemId = null)
        {
            return _context.SupplierItems.Any(si =>
                si.SupplierId == supplierId &&
                si.ItemId == itemId &&
                (!excludeSupplierItemId.HasValue ||
                 si.SupplierItemId !=
                    excludeSupplierItemId.Value)
            );
        }


        public bool SupplierExists(int supplierId)
        {
            return _context.Suppliers.Any(s =>
                s.SupplierId == supplierId
            );
        }


        public bool ItemExists(int itemId)
        {
            return _context.Items.Any(i =>
                i.ItemId == itemId
            );
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}