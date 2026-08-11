using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharma.Dal.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly PharmacyDbContext _context;
        public CategoryRepository(PharmacyDbContext con)
        {
            _context = con;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c=> c.CategoryId==id);
        }
        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public void AddCategory(Category cat) {
            _context.Categories.Add(cat);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public bool CategoryExists(string categoryName, int? ExcludeCategoryId=null) {
            return _context.Categories.Any(c => c.CategoryName == categoryName && (!ExcludeCategoryId.HasValue || c.CategoryId != ExcludeCategoryId));
        }
    }
}
