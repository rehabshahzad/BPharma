using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class SupplierItemService : ISupplierItemService
    {
        private readonly ISupplierItemRepository _repository;

        public SupplierItemService(
            ISupplierItemRepository repository)
        {
            _repository = repository;
        }


        public List<SupplierItem> GetAllSupplierItems()
        {
            return _repository.GetAllSupplierItems();
        }


        public SupplierItem GetSupplierItemById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "SupplierItem id is invalid."
                );
            }

            var supplierItem =
                _repository.GetSupplierItemById(id);

            if (supplierItem == null)
            {
                throw new KeyNotFoundException(
                    "Supplier item does not exist."
                );
            }

            return supplierItem;
        }


        public SupplierItem CreateSupplierItem(
            SupplierItem supplierItem,
            int createdByEmployeeId)
        {
            ValidateSupplierItem(supplierItem);

            if (createdByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "CreatedBy EmployeeId is invalid."
                );
            }

            ValidateRelationships(supplierItem);


            if (_repository.SupplierItemExists(
                supplierItem.SupplierId,
                supplierItem.ItemId))
            {
                throw new InvalidOperationException(
                    "This supplier already supplies this item."
                );
            }


            supplierItem.IsActive = true;

            supplierItem.CreatedByEmployeeId =
                createdByEmployeeId;

            supplierItem.CreatedAt =
                DateTime.Now;


            _repository.Add(supplierItem);

            _repository.SaveChanges();

            return supplierItem;
        }


        public SupplierItem UpdateSupplierItem(
            int id,
            SupplierItem supplierItem,
            int updatedByEmployeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "SupplierItem id is invalid."
                );
            }

            if (updatedByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "UpdatedBy EmployeeId is invalid."
                );
            }


            ValidateSupplierItem(supplierItem);


            var existing =
                _repository.GetSupplierItemById(id);


            if (existing == null)
            {
                throw new KeyNotFoundException(
                    "Supplier item does not exist."
                );
            }


            ValidateRelationships(supplierItem);


            if (_repository.SupplierItemExists(
                supplierItem.SupplierId,
                supplierItem.ItemId,
                id))
            {
                throw new InvalidOperationException(
                    "This supplier already supplies this item."
                );
            }


            existing.SupplierId =
                supplierItem.SupplierId;

            existing.ItemId =
                supplierItem.ItemId;

            existing.SupplierPrice =
                supplierItem.SupplierPrice;

            existing.IsActive =
                supplierItem.IsActive;

            existing.UpdatedByEmployeeId =
                updatedByEmployeeId;

            existing.UpdatedAt =
                DateTime.Now;


            _repository.SaveChanges();

            return existing;
        }


        private void ValidateSupplierItem(
            SupplierItem supplierItem)
        {
            if (supplierItem == null)
            {
                throw new ArgumentNullException(
                    nameof(supplierItem),
                    "Supplier item data is required."
                );
            }


            if (supplierItem.SupplierId <= 0)
            {
                throw new ArgumentException(
                    "Supplier id is invalid."
                );
            }


            if (supplierItem.ItemId <= 0)
            {
                throw new ArgumentException(
                    "Item id is invalid."
                );
            }


            if (supplierItem.SupplierPrice <= 0)
            {
                throw new ArgumentException(
                    "Supplier price cannot be negative."
                );
            }
        }


        private void ValidateRelationships(
            SupplierItem supplierItem)
        {
            if (!_repository.SupplierExists(
                supplierItem.SupplierId))
            {
                throw new KeyNotFoundException(
                    "Supplier does not exist."
                );
            }


            if (!_repository.ItemExists(
                supplierItem.ItemId))
            {
                throw new KeyNotFoundException(
                    "Item does not exist."
                );
            }
        }
    }
}