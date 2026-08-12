using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly PharmacyDbContext _context;

        public ItemRepository(
            PharmacyDbContext context)
        {
            _context = context;
        }


        public List<Item> GetAllItems()
        {
            return _context.Items
                .ToList();
        }


        public Item GetItemById(int id)
        {
            return _context.Items
                .FirstOrDefault(
                    i => i.ItemId == id
                );
        }


        public void Add(Item item)
        {
            _context.Items.Add(item);
        }


        public bool BarcodeExists(
            string barcode,
            int? excludeItemId = null)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return false;
            }

            return _context.Items.Any(i =>
                i.Barcode == barcode &&
                (!excludeItemId.HasValue ||
                 i.ItemId != excludeItemId.Value)
            );
        }


        public bool CategoryExists(int categoryId)
        {
            return _context.Categories
                .Any(c =>
                    c.CategoryId == categoryId
                );
        }


        public bool BrandExists(int brandId)
        {
            return _context.Brands
                .Any(b =>
                    b.BrandId == brandId
                );
        }


        public bool FormulaExists(int formulaId)
        {
            return _context.Formulas
                .Any(f =>
                    f.FormulaId == formulaId
                );
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}