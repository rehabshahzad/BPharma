using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharma.Entity.Entities;
using Pharma.DAL.Context;
namespace Pharma.BLL.Services
{
public interface  ICustomerService
    {
        Customer GetCustomerById(int id);
        List<Customer> GetAllCustomers();
        Customer CreateCustomer(Customer customer, int CreatedByEmployeeId);
        Customer UpdateCustomer(int id,Customer customer, int UpdatedByEmployeeId);
    }
}
