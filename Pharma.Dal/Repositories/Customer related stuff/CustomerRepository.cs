using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PharmacyDbContext _context;

        public CustomerRepository(
            PharmacyDbContext context)
        {
            _context = context;
        }


        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }


        public Customer GetCustomerById(int id)
        {
            return _context.Customers
                .FirstOrDefault(c =>
                    c.CustomerId == id
                );
        }


        public bool CustomerExists(
            string contact,
            int? excludeCustomerId = null)
        {
            return _context.Customers.Any(c =>
                c.Contact == contact &&
                (!excludeCustomerId.HasValue ||
                 c.CustomerId != excludeCustomerId.Value)
            );
        }


        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}