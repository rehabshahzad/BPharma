
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly PharmacyDbContext _context;

        public BrandRepository(PharmacyDbContext context)
        {
            _context = context;
        }


        public Brand GetBrandById(int id)
        {
            return _context.Brands
                .FirstOrDefault(b => b.BrandId == id);
        }


        public List<Brand> GetAllBrands()
        {
            return _context.Brands
                .AsNoTracking()
                .ToList();
        }


        public void AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
        }


        public bool BrandExists(
            string brandName,
            int? excludeBrandId = null)
        {
            return _context.Brands.Any(b =>
                b.BrandName == brandName &&
                (!excludeBrandId.HasValue ||
                 b.BrandId != excludeBrandId.Value)
            );
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
