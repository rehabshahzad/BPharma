using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Pharma.BLL.Services
{
    public class PurchaseService : IPurchaseService
    {

        private readonly IPurchaseRepository _repository;
        public PurchaseService(IPurchaseRepository repository)
        {
            _repository = repository;
        }
        public List<Purchase> GetAllPurchases()
        {
            return _repository.GetAllPurchases();
        }
        public Purchase GetPurchaseById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Purchase id is invalid");
            }
            var purchase = _repository.GetPurchaseById(id);
            if (purchase == null)
            {
                throw new KeyNotFoundException("This Purchase doesn't exist");
            }
            return purchase;
        }
        public Purchase CreatePurchase(
        Purchase purchase,
        List<PurchaseItem> items,
        int employeeId)
        {
            // Purchase object itself must exist
            if (purchase == null)
            {
                throw new ArgumentNullException(
                    nameof(purchase),
                    "Purchase data is required."
                );
            }


            // Employee id must at least be a valid positive number
            if (employeeId <= 0)
            {
                throw new ArgumentException(
                    "Employee id is invalid."
                );
            }


            // Check that employee actually exists
            var employee =
                _repository.GetEmployeeById(employeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException(
                    "Employee does not exist."
                );
            }


            // Supplier id validation
            if (purchase.SupplierId <= 0)
            {
                throw new ArgumentException(
                    "Supplier id is invalid."
                );
            }


            // Supplier must actually exist
            var supplier =
                _repository.GetSupplierById(
                    purchase.SupplierId
                );

            if (supplier == null)
            {
                throw new KeyNotFoundException(
                    "Supplier does not exist."
                );
            }


            // A purchase without items makes no sense
            if (items == null || items.Count == 0)
            {
                throw new ArgumentException(
                    "Purchase must contain at least one item."
                );
            }


            // Extra charges may be zero, but never negative
            if (purchase.AdditionalCharges < 0)
            {
                throw new ArgumentException(
                    "Additional charges cannot be negative."
                );
            }


            // Prevent same item appearing twice
            var duplicateItemExists =
                items.GroupBy(i => i.ItemId)
                     .Any(g => g.Count() > 1);

            if (duplicateItemExists)
            {
                throw new InvalidOperationException(
                    "The same item cannot appear more than once in a purchase."
                );
            }


            decimal subtotal = 0;


            foreach (var item in items)
            {
                if (item == null)
                {
                    throw new ArgumentException(
                        "Purchase item cannot be null."
                    );
                }


                if (item.ItemId <= 0)
                {
                    throw new ArgumentException(
                        "Item id is invalid."
                    );
                }


                if (item.OrderedQuantity <= 0)
                {
                    throw new ArgumentException(
                        "Ordered quantity must be greater than zero."
                    );
                }


                if (item.UnitPurchasePrice <= 0)
                {
                    throw new ArgumentException(
                        "Unit purchase price must be greater than zero."
                    );
                }


                // Item must exist
                var existingItem =
                    _repository.GetItemById(
                        item.ItemId
                    );

                if (existingItem == null)
                {
                    throw new KeyNotFoundException(
                        $"Item {item.ItemId} does not exist."
                    );
                }


                // Supplier must actually be registered as supplying this item
                if (!_repository.SupplierSuppliesItem(
                    purchase.SupplierId,
                    item.ItemId))
                {
                    throw new InvalidOperationException(
                        $"Supplier does not supply item {item.ItemId}."
                    );
                }


                // quantity × unit price
                subtotal +=
                    item.OrderedQuantity *
                    item.UnitPurchasePrice;
            }


            // Backend-calculated financial values
            purchase.SubtotalAmount =
                subtotal;

            purchase.TotalAmount =
                purchase.SubtotalAmount +
                purchase.AdditionalCharges;


            // Backend-controlled audit / time values
            purchase.PurchaseDate =
                DateTime.Now;

            purchase.CreatedAt =
                DateTime.Now;

            purchase.CreatedByEmployeeId =
                employeeId;

            purchase.Notes =
                purchase.Notes?.Trim();


            // Save Purchase first so DB generates PurchaseId
            _repository.AddPurchase(purchase);

            _repository.SaveChanges();


            // Connect every PurchaseItem to newly-created Purchase
            foreach (var item in items)
            {
                item.PurchaseId =
                    purchase.PurchaseId;

                _repository.AddPurchaseItem(item);
            }


            _repository.SaveChanges();

            return purchase;
        }
        public Purchase UpdatePurchase(
    int id,
    Purchase purchase,
    int employeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Purchase id is invalid."
                );
            }


            if (purchase == null)
            {
                throw new ArgumentNullException(
                    nameof(purchase),
                    "Purchase data is required."
                );
            }


            if (employeeId <= 0)
            {
                throw new ArgumentException(
                    "Employee id is invalid."
                );
            }


            // Confirm employee exists
            var employee =
                _repository.GetEmployeeById(
                    employeeId
                );

            if (employee == null)
            {
                throw new KeyNotFoundException(
                    "Employee does not exist."
                );
            }


            // Get existing tracked Purchase
            var existingPurchase =
                _repository.GetPurchaseById(id);


            if (existingPurchase == null)
            {
                throw new KeyNotFoundException(
                    "Purchase does not exist."
                );
            }


            if (purchase.AdditionalCharges < 0)
            {
                throw new ArgumentException(
                    "Additional charges cannot be negative."
                );
            }


            // Fields user is allowed to modify
            existingPurchase.AdditionalCharges =
                purchase.AdditionalCharges;

            existingPurchase.Notes =
                purchase.Notes?.Trim();

            existingPurchase.Status =
                purchase.Status;


            // Subtotal hasn't changed because items haven't changed.
            // But additional charges may have changed,
            // so TotalAmount must be recalculated.
            existingPurchase.TotalAmount =
                existingPurchase.SubtotalAmount +
                existingPurchase.AdditionalCharges;


            // Audit
            existingPurchase.UpdatedByEmployeeId =
                employeeId;

            existingPurchase.UpdatedAt =
                DateTime.Now;


            _repository.SaveChanges();

            return existingPurchase;
        }
    }
}

