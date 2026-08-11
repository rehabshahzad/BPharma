using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;



namespace Pharma.Dal.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategoryById(int id);
        List<Category> GetAllCategories();
        void AddCategory(Category cat);
        void SaveChanges();
        bool CategoryExists(String cat, int? ExcludedCategoryId=null);
    }
}
