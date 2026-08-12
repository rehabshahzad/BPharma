using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharma.Entity.Entities;
using Pharma.DAL.Context;

namespace Pharma.Dal.Repositories
{
    public class CustomerRepository : ICustomerRepository

    {
        private readonly PharmacyDbContext _context;
        public CustomerRepository(PharmacyDbContext context)
        {
            _context = context;
        }
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.CustomerId == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.AsNoTracking().ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Add(Customer customer) {
            _context.Customers.Add(customer);
        }

        public bool CustomerExists(string Contact, int? excludeCustomerId = null)
        {
            return _context.Customers.Any(c=> c.Contact== Contact && ( !excludeCustomerId.HasValue || excludeCustomerId.Value != c.CustomerId));
        } 
        }

    }
