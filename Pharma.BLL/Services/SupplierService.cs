using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Pharma.BLL.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;


        public SupplierService(
            ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }


        // GET BY ID
        public Supplier GetSupplierById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid supplier ID."
                );
            }

            var supplier =
                _supplierRepository.GetSupplierById(id);

            if (supplier == null)
            {
                throw new KeyNotFoundException(
                    "Supplier not found."
                );
            }

            return supplier;
        }


        // GET ALL
        public List<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAllSuppliers();
        }


        // CREATE
        public Supplier CreateSupplier(
            Supplier supplier,
            int createdByEmployeeId)
        {
            ValidateSupplier(supplier);

            if (createdByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Created-by employee ID is required."
                );
            }

            supplier.SupplierName =
                supplier.SupplierName.Trim();

            supplier.ContactPersonName =
                supplier.ContactPersonName.Trim();

            supplier.ContactNumber =
                supplier.ContactNumber.Trim();

            supplier.Email =
                supplier.Email.Trim();

            supplier.Address =
                supplier.Address.Trim();


            if (_supplierRepository.SupplierExists(
                    supplier.SupplierName,
                    supplier.Email))
            {
                throw new InvalidOperationException(
                    "Supplier already exists."
                );
            }


            supplier.IsActive = true;

            supplier.CreatedByEmployeeId =
                createdByEmployeeId;

            supplier.CreatedAt =
                DateTime.Now;

            supplier.UpdatedByEmployeeId =
                null;

            supplier.UpdatedAt =
                null;


            _supplierRepository.AddSupplier(
                supplier
            );

            _supplierRepository.SaveChanges();

            return supplier;
        }


        // UPDATE
        public Supplier UpdateSupplier(
            int id,
            Supplier updatedSupplier,
            int updatedByEmployeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid supplier ID."
                );
            }

            if (updatedByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Updated-by employee ID is required."
                );
            }

            ValidateSupplier(updatedSupplier);


            var existingSupplier =
                _supplierRepository.GetSupplierById(id);

            if (existingSupplier == null)
            {
                throw new KeyNotFoundException(
                    "Supplier not found."
                );
            }


            updatedSupplier.SupplierName =
                updatedSupplier.SupplierName.Trim();

            updatedSupplier.ContactPersonName =
                updatedSupplier.ContactPersonName.Trim();

            updatedSupplier.ContactNumber =
                updatedSupplier.ContactNumber.Trim();

            updatedSupplier.Email =
                updatedSupplier.Email.Trim();

            updatedSupplier.Address =
                updatedSupplier.Address.Trim();


            if (_supplierRepository.SupplierExists(
                    updatedSupplier.SupplierName,
                    updatedSupplier.Email,
                    id))
            {
                throw new InvalidOperationException(
                    "Supplier already exists."
                );
            }


            existingSupplier.SupplierName =
                updatedSupplier.SupplierName;

            existingSupplier.ContactPersonName =
                updatedSupplier.ContactPersonName;

            existingSupplier.ContactNumber =
                updatedSupplier.ContactNumber;

            existingSupplier.Email =
                updatedSupplier.Email;

            existingSupplier.Address =
                updatedSupplier.Address;

            existingSupplier.IsActive =
                updatedSupplier.IsActive;

            existingSupplier.UpdatedByEmployeeId =
                updatedByEmployeeId;

            existingSupplier.UpdatedAt =
                DateTime.Now;


            _supplierRepository.SaveChanges();

            return existingSupplier;
        }


        // COMMON VALIDATION
        private void ValidateSupplier(
            Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(
                    nameof(supplier),
                    "Supplier data is required."
                );
            }


            if (string.IsNullOrWhiteSpace(
                    supplier.SupplierName))
            {
                throw new ArgumentException(
                    "Supplier name is required."
                );
            }

            if (supplier.SupplierName.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Supplier name should not exceed 100 characters."
                );
            }


            if (string.IsNullOrWhiteSpace(
                    supplier.ContactPersonName))
            {
                throw new ArgumentException(
                    "Contact person name is required."
                );
            }

            if (supplier.ContactPersonName.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Contact person name should not exceed 100 characters."
                );
            }


            if (string.IsNullOrWhiteSpace(
                    supplier.ContactNumber))
            {
                throw new ArgumentException(
                    "Contact number is required."
                );
            }

            if (supplier.ContactNumber.Trim().Length > 20)
            {
                throw new ArgumentException(
                    "Contact number should not exceed 20 characters."
                );
            }


            if (string.IsNullOrWhiteSpace(
                    supplier.Email))
            {
                throw new ArgumentException(
                    "Email is required."
                );
            }

            if (supplier.Email.Trim().Length > 150)
            {
                throw new ArgumentException(
                    "Email should not exceed 150 characters."
                );
            }

            if (!IsValidEmail(supplier.Email))
            {
                throw new ArgumentException(
                    "Email format is invalid."
                );
            }


            if (string.IsNullOrWhiteSpace(
                    supplier.Address))
            {
                throw new ArgumentException(
                    "Address is required."
                );
            }

            if (supplier.Address.Trim().Length > 250)
            {
                throw new ArgumentException(
                    "Address should not exceed 250 characters."
                );
            }
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var address =
                    new MailAddress(email.Trim());

                return address.Address ==
                       email.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}