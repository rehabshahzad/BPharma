using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;

namespace Pharma.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repo)
        {
            _repository = repo;
        }
        public Customer GetCustomerById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Customer id must be greater than 0");
            }
            var cust = _repository.GetCustomerById(id);
            if(cust == null)
            {
                throw new KeyNotFoundException("Customer with this id does not exist.");
            }
            return cust;

        }
        public List<Customer> GetAllCustomers()
        {
            return _repository.GetAllCustomers();
        }

        public Customer CreateCustomer(Customer customer, int CreatedByEmployeeId)
        {
            ValidateCustomer(customer);

            if (CreatedByEmployeeId <= 0)
            {
                throw new ArgumentException("Created by Employee id is invalid");
            }

            //Trimming before duplicate checking
            customer.Contact= customer.Contact.Trim();
            customer.FirstName = customer.FirstName.Trim();
            customer.LastName = customer.LastName.Trim();
            customer.Contact = customer.Contact.Trim();
            customer.Email = customer.Email?.Trim();
            customer.Address = customer.Address?.Trim();

            if (_repository.CustomerExists(customer.Contact, null))
            {
                throw new ArgumentException("This Customer already exists");
            }
            customer.CreatedByEmployeeId= CreatedByEmployeeId;
            customer.CreatedAt = DateTime.Now;
            _repository.Add(customer);
            _repository.SaveChanges();
            return customer;

           
        }

        public Customer UpdateCustomer(int id, Customer customer, int UpdatedByEmployeeId)
        {

            if(id <= 0)
            {
                throw new ArgumentException("Enter a valid Customer Id");
            }
            if(UpdatedByEmployeeId <= 0)
            {
                throw new ArgumentException("UpdatedBy EmployeeId is invalid");
            }

            ValidateCustomer(customer);
            var existingCustomer = _repository.GetCustomerById(id);
           
            if (existingCustomer == null) {
                throw new KeyNotFoundException("Existing customer is null");
            }
           
            customer.Contact=customer.Contact.Trim();

            if (_repository.CustomerExists(customer.Contact, id))
            {
                throw new ArgumentException("This Customer already exists");
            }
            existingCustomer.FirstName = customer.FirstName.Trim();
            existingCustomer.LastName = customer.LastName.Trim();
            existingCustomer.Contact = customer.Contact.Trim();
            existingCustomer.Email = customer.Email?.Trim();
            existingCustomer.Address = customer.Address.Trim();

            existingCustomer.UpdatedByEmployeeId = UpdatedByEmployeeId;
            existingCustomer.UpdatedAt = DateTime.Now;

           

          
            _repository.SaveChanges();
            return existingCustomer;


        }

        private void ValidateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Cannot process null customer");

            }

            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                throw new ArgumentException("Customer First name is required");
            }

            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                throw new ArgumentException("Customer Last name is required");
            }

            if (string.IsNullOrWhiteSpace(customer.Contact))
            {
                throw new ArgumentException("Customer Contact number is required");
            }

            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                throw new ArgumentException("Customer Address is required");
            }




        }
    }
}