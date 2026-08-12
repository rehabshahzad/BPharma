using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(
            IItemRepository repository)
        {
            _repository = repository;
        }


        public List<Item> GetAllItems()
        {
            return _repository.GetAllItems();
        }


        public Item GetItemById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Item id must be greater than 0."
                );
            }

            var item =
                _repository.GetItemById(id);

            if (item == null)
            {
                throw new KeyNotFoundException(
                    "Item with this id does not exist."
                );
            }

            return item;
        }


        public Item CreateItem(
            Item item,
            int createdByEmployeeId)
        {
            ValidateItem(item);

            if (createdByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "CreatedBy EmployeeId is invalid."
                );
            }


            ValidateRelationships(item);


            item.ItemName =
                item.ItemName.Trim();

            item.Description =
                item.Description?.Trim();

            item.PictureUrl =
                item.PictureUrl?.Trim();

            item.Barcode =
                item.Barcode?.Trim();

            item.RackNumber =
                item.RackNumber?.Trim();

            item.ShelfNumber =
                item.ShelfNumber?.Trim();

            item.LaneNumber =
                item.LaneNumber?.Trim();


            if (_repository.BarcodeExists(
                item.Barcode))
            {
                throw new InvalidOperationException(
                    "An item with this barcode already exists."
                );
            }


            item.IsActive = true;

            item.CreatedByEmployeeId =
                createdByEmployeeId;

            item.CreatedAt =
                DateTime.Now;


            _repository.Add(item);

            _repository.SaveChanges();

            return item;
        }


        public Item UpdateItem(
            int id,
            Item item,
            int updatedByEmployeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid Item Id."
                );
            }

            if (updatedByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "UpdatedBy EmployeeId is invalid."
                );
            }


            ValidateItem(item);

            ValidateRelationships(item);


            var existingItem =
                _repository.GetItemById(id);


            if (existingItem == null)
            {
                throw new KeyNotFoundException(
                    "Item with this id does not exist."
                );
            }


            item.ItemName =
                item.ItemName.Trim();

            item.Barcode =
                item.Barcode?.Trim();


            if (_repository.BarcodeExists(
                item.Barcode,
                id))
            {
                throw new InvalidOperationException(
                    "An item with this barcode already exists."
                );
            }


            existingItem.CategoryId =
                item.CategoryId;

            existingItem.BrandId =
                item.BrandId;

            existingItem.FormulaId =
                item.FormulaId;

            existingItem.ItemName =
                item.ItemName;

            existingItem.Description =
                item.Description?.Trim();

            existingItem.PictureUrl =
                item.PictureUrl?.Trim();

            existingItem.Barcode =
                item.Barcode;

            existingItem.IsPrescriptionRequired =
                item.IsPrescriptionRequired;

            existingItem.SellingPrice =
                item.SellingPrice;

            existingItem.MinimumStockLevel =
                item.MinimumStockLevel;

            existingItem.RackNumber =
                item.RackNumber?.Trim();

            existingItem.ShelfNumber =
                item.ShelfNumber?.Trim();

            existingItem.LaneNumber =
                item.LaneNumber?.Trim();

            existingItem.IsActive =
                item.IsActive;

            existingItem.UpdatedByEmployeeId =
                updatedByEmployeeId;

            existingItem.UpdatedAt =
                DateTime.Now;


            _repository.SaveChanges();

            return existingItem;
        }


        private void ValidateItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(
                    nameof(item),
                    "Item data is required."
                );
            }


            if (string.IsNullOrWhiteSpace(
                item.ItemName))
            {
                throw new ArgumentException(
                    "Item name is required."
                );
            }


            if (item.CategoryId <= 0)
            {
                throw new ArgumentException(
                    "Category id is invalid."
                );
            }


            if (item.BrandId <= 0)
            {
                throw new ArgumentException(
                    "Brand id is invalid."
                );
            }


            if (item.FormulaId.HasValue &&
                item.FormulaId.Value <= 0)
            {
                throw new ArgumentException(
                    "Formula id is invalid."
                );
            }


            if (item.SellingPrice < 0)
            {
                throw new ArgumentException(
                    "Selling price cannot be negative."
                );
            }


            if (item.MinimumStockLevel < 0)
            {
                throw new ArgumentException(
                    "Minimum stock level cannot be negative."
                );
            }
        }


        private void ValidateRelationships(
            Item item)
        {
            if (!_repository.CategoryExists(
                item.CategoryId))
            {
                throw new KeyNotFoundException(
                    "Category does not exist."
                );
            }


            if (!_repository.BrandExists(
                item.BrandId))
            {
                throw new KeyNotFoundException(
                    "Brand does not exist."
                );
            }


            if (item.FormulaId.HasValue &&
                !_repository.FormulaExists(
                    item.FormulaId.Value))
            {
                throw new KeyNotFoundException(
                    "Formula does not exist."
                );
            }
        }
    }
}