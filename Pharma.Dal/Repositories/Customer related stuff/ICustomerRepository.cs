using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();

        Customer GetCustomerById(int id);

        bool CustomerExists(
            string contact,
            int? excludeCustomerId = null
        );

        void Add(Customer customer);

        void SaveChanges();
    }
}