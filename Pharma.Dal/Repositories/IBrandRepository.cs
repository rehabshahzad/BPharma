using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Pharma.Dal.Repositories
{
    public interface IBrandRepository
    {
         Brand GetBrandById(int id);

        List<Brand> GetAllBrands();

        void AddBrand(Brand brand);

        bool BrandExists(
            string brandName,
            int? excludeBrandId = null
        );

        void SaveChanges();
    }
}
