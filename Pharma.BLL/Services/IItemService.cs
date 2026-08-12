using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface IItemService
    {
        List<Item> GetAllItems();

        Item GetItemById(int id);

        Item CreateItem(
            Item item,
            int createdByEmployeeId
        );

        Item UpdateItem(
            int id,
            Item item,
            int updatedByEmployeeId
        );
    }
}