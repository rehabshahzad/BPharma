using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class SupplierReturnService
        : ISupplierReturnService
    {
        private readonly ISupplierReturnRepository _repository;

        public SupplierReturnService(
            ISupplierReturnRepository repository)
        {
            _repository = repository;
        }


        public List<SupplierReturn>
            GetAllSupplierReturns()
        {
            return _repository
                .GetAllSupplierReturns();
        }


        public SupplierReturn
            GetSupplierReturnById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Supplier return id is invalid."
                );
            }

            var supplierReturn =
                _repository.GetSupplierReturnById(id);

            if (supplierReturn == null)
            {
                throw new KeyNotFoundException(
                    "Supplier return does not exist."
                );
            }

            return supplierReturn;
        }


        public SupplierReturn CreateSupplierReturn(
            SupplierReturn supplierReturn,
            List<SupplierReturnItem> items,
            int employeeId)
        {
            if (supplierReturn == null)
            {
                throw new ArgumentNullException(
                    nameof(supplierReturn)
                );
            }

            if (employeeId <= 0)
            {
                throw new ArgumentException(
                    "Employee id is invalid."
                );
            }

            if (supplierReturn.PurchaseId <= 0)
            {
                throw new ArgumentException(
                    "Purchase id is invalid."
                );
            }

            if (items == null || items.Count == 0)
            {
                throw new ArgumentException(
                    "At least one return item is required."
                );
            }


            var purchase =
                _repository.GetPurchaseById(
                    supplierReturn.PurchaseId
                );

            if (purchase == null)
            {
                throw new KeyNotFoundException(
                    "Purchase does not exist."
                );
            }


            supplierReturn.Reason =
                supplierReturn.Reason?.Trim();

            supplierReturn.CreatedByEmployeeId =
                employeeId;

            supplierReturn.CreatedAt =
                DateTime.Now;

            supplierReturn.ReturnDate =
                DateTime.Now;


            _repository.AddReturn(
                supplierReturn
            );

            _repository.SaveChanges();


            foreach (var item in items)
            {
                ValidateReturnItem(
                    item,
                    supplierReturn.PurchaseId
                );

                item.SupplierReturnId =
                    supplierReturn.SupplierReturnId;

                item.Reason =
                    item.Reason?.Trim();

                _repository.AddReturnItem(item);
            }


            _repository.SaveChanges();

            return supplierReturn;
        }


        public SupplierReturn UpdateSupplierReturn(
            int id,
            SupplierReturn supplierReturn,
            int employeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Supplier return id is invalid."
                );
            }

            if (employeeId <= 0)
            {
                throw new ArgumentException(
                    "Employee id is invalid."
                );
            }

            if (supplierReturn == null)
            {
                throw new ArgumentNullException(
                    nameof(supplierReturn)
                );
            }


            var existing =
                _repository.GetSupplierReturnById(id);

            if (existing == null)
            {
                throw new KeyNotFoundException(
                    "Supplier return does not exist."
                );
            }


            existing.Reason =
                supplierReturn.Reason?.Trim();

            existing.Status =
                supplierReturn.Status;

            existing.UpdatedByEmployeeId =
                employeeId;

            existing.UpdatedAt =
                DateTime.Now;


            _repository.SaveChanges();

            return existing;
        }


        private void ValidateReturnItem(
            SupplierReturnItem item,
            int purchaseId)
        {
            if (item == null)
            {
                throw new ArgumentException(
                    "Supplier return item is required."
                );
            }

            if (item.BatchId <= 0)
            {
                throw new ArgumentException(
                    "Batch id is invalid."
                );
            }

            if (item.ReturnQuantity <= 0)
            {
                throw new ArgumentException(
                    "Return quantity must be greater than zero."
                );
            }

            if (item.ReturnAmount < 0)
            {
                throw new ArgumentException(
                    "Return amount cannot be negative."
                );
            }


            var batch =
                _repository.GetBatchById(
                    item.BatchId
                );

            if (batch == null)
            {
                throw new KeyNotFoundException(
                    "Batch does not exist."
                );
            }


            if (!_repository.BatchBelongsToPurchase(
                item.BatchId,
                purchaseId))
            {
                throw new InvalidOperationException(
                    "This batch does not belong to the selected purchase."
                );
            }
        }
    }
}