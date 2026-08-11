using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharma.Entity.Entities;

namespace Pharma.BLL.Services
{
    public interface IBrandService
    {
        Brand GetBrandById(int id);

        List<Brand> GetAllBrands();

        Brand CreateBrand(
            Brand brand,
            int createdByEmployeeId
        );

        Brand UpdateBrand(
            int id,
            Brand brand,
            int updatedByEmployeeId
        );
    }
}
