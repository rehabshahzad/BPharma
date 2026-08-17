using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface ISaleService
    {
        List<Sale> GetAllSales();

        Sale GetSaleById(int id);

        Sale CreateSale(
            Sale sale,
            List<SaleItem> items,
            int employeeId
        );

        Sale UpdateSale(
            int id,
            Sale sale,
            int employeeId
        );
    }
}