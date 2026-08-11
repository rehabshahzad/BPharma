using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface ICategoryService
    {
        Category GetCategoryById(int id);

        List<Category> GetAllCategories();

        Category CreateCategory(
            Category cat,
            int createdByEmployeeId
        );

        Category UpdateCategory(
            int id,
            Category cat,
            int updatedByEmployeeId
        );
    }
}