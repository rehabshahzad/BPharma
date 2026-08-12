using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface IItemRepository
    {
        List<Item> GetAllItems();

        Item GetItemById(int id);

        void Add(Item item);

        bool BarcodeExists(
            string barcode,
            int? excludeItemId = null
        );

        bool CategoryExists(int categoryId);

        bool BrandExists(int brandId);

        bool FormulaExists(int formulaId);

        void SaveChanges();
    }
}