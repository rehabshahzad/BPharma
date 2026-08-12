using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharma.Entity.Entities;

namespace Pharma.Dal.Repositories
{
    public interface ICustomerRepository
    {
         Customer GetCustomerById(int id);
        List<Customer> GetAllCustomers();
        bool CustomerExists(string Contact, int? excludeCustomerId = null);
        void SaveChanges();
        void Add(Customer customer);



    }
}
