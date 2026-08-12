using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface ICustomerReturnService
    {
        List<CustomerReturn> GetAllCustomerReturns();

        CustomerReturn GetCustomerReturnById(int id);

        CustomerReturn CreateCustomerReturn(
            CustomerReturn customerReturn,
            List<CustomerReturnItem> items,
            int employeeId
        );

        CustomerReturn UpdateCustomerReturn(
            int id,
            CustomerReturn customerReturn,
            int employeeId
        );
    }
}