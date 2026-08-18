using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.BLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;

        public SaleService(ISaleRepository repository)
        {
            _repository = repository;
        }


        public List<Sale> GetAllSales()
        {
            return _repository.GetAllSales();
        }


        public Sale GetSaleById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Sale id is invalid."
                );
            }

            var sale = _repository.GetSaleById(id);

            if (sale == null)
            {
                throw new KeyNotFoundException(
                    "Sale does not exist."
                );
            }

            return sale;
        }


        public Sale CreateSale(
            Sale sale,
            List<SaleItem> items,
            int employeeId)
        {
            ValidateSale(sale, employeeId);


            // Customer validation
            if (sale.CustomerId <= 0)
            {
                throw new ArgumentException(
                    "Customer id is invalid."
                );
            }

            var customer =
                _repository.GetCustomerById(
                    sale.CustomerId
                );

            if (customer == null)
            {
                throw new KeyNotFoundException(
                    "Customer does not exist."
                );
            }


            // Must contain at least one item
            if (items == null || items.Count == 0)
            {
                throw new ArgumentException(
                    "Sale must contain at least one item."
                );
            }


            // Prevent same item appearing multiple times
            var duplicateItemExists =
                items
                    .Where(i => i != null)
                    .GroupBy(i => i.ItemId)
                    .Any(g => g.Count() > 1);

            if (duplicateItemExists)
            {
                throw new InvalidOperationException(
                    "The same item cannot appear more than once in a sale."
                );
            }


            decimal subtotal = 0;


            /*
             * Validate SaleItems and calculate
             * their prices BEFORE starting DB transaction.
             */
            foreach (var saleItem in items)
            {
                ValidateSaleItem(saleItem);


                var item =
                    _repository.GetItemById(
                        saleItem.ItemId
                    );

                if (item == null)
                {
                    throw new KeyNotFoundException(
                        $"Item {saleItem.ItemId} does not exist."
                    );
                }


                if (!item.IsActive)
                {
                    throw new InvalidOperationException(
                        $"Item {saleItem.ItemId} is inactive and cannot be sold."
                    );
                }


                /*
                 * Backend controls the selling price.
                 * We do NOT trust UnitSalePrice
                 * coming from the DTO/client.
                 */
                saleItem.UnitSalePrice =
                    item.SellingPrice;


                subtotal +=
                    saleItem.OrderedQuantity *
                    saleItem.UnitSalePrice;
            }


            // Backend-calculated amounts
            sale.SubtotalAmount =
                subtotal;

            sale.TotalAmount =
                sale.SubtotalAmount +
                sale.AdditionalCharges;


            // Backend-controlled fields
            sale.SaleDate =
                DateTime.Now;

            sale.SoldAt =
                DateTime.Now;

            sale.SoldByEmployeeId =
                employeeId;

            sale.Notes =
                sale.Notes?.Trim();


            /*
             * Everything below is one transaction.
             *
             * Sale
             * +
             * SaleItems
             * +
             * BatchAllocations
             *
             * Either all succeed or none are saved.
             */
            _repository.BeginTransaction();

            try
            {
                // -------------------------
                // 1. SAVE SALE
                // -------------------------

                _repository.AddSale(sale);

                _repository.SaveChanges();


                /*
                 * After SaveChanges(),
                 * database-generated SaleId
                 * is now available.
                 */


                // -------------------------
                // 2. SAVE SALE ITEMS
                // -------------------------

                foreach (var saleItem in items)
                {
                    saleItem.SaleId =
                        sale.SaleId;

                    _repository.AddSaleItem(
                        saleItem
                    );
                }

                _repository.SaveChanges();


                /*
                 * After this SaveChanges(),
                 * every SaleItem now has its
                 * generated SaleItemId.
                 */


                // -------------------------
                // 3. ALLOCATE BATCHES
                // -------------------------

                foreach (var saleItem in items)
                {
                    AllocateBatches(
                        saleItem, employeeId
                    );
                }


                // Save BatchAllocation records
                _repository.SaveChanges();


                // Everything succeeded
                _repository.CommitTransaction();

                return sale;
            }
            catch
            {
                /*
                 * Something failed:
                 *
                 * Sale is undone
                 * SaleItems are undone
                 * BatchAllocations are undone
                 */
                _repository.RollbackTransaction();

                throw;
            }
        }


        public Sale UpdateSale(
            int id,
            Sale sale,
            int employeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Sale id is invalid."
                );
            }


            ValidateSale(
                sale,
                employeeId
            );


            var existingSale =
                _repository.GetSaleById(id);

            if (existingSale == null)
            {
                throw new KeyNotFoundException(
                    "Sale does not exist."
                );
            }


            /*
             * V1:
             * We only allow these fields
             * to change.
             */

            existingSale.AdditionalCharges =
                sale.AdditionalCharges;

            existingSale.Notes =
                sale.Notes?.Trim();

            existingSale.Status =
                sale.Status;


            /*
             * SaleItems are NOT being changed,
             * therefore SubtotalAmount stays
             * exactly the same.
             *
             * Only TotalAmount needs recalculation
             * because AdditionalCharges may change.
             */
            existingSale.TotalAmount =
                existingSale.SubtotalAmount +
                existingSale.AdditionalCharges;


            // Audit
            existingSale.UpdatedByEmployeeId =
                employeeId;

            existingSale.UpdatedAt =
                DateTime.Now;


            _repository.SaveChanges();

            return existingSale;
        }


        // ===================================================
        // COMMON SALE VALIDATION
        // ===================================================

        private void ValidateSale(
            Sale sale,
            int employeeId)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(
                    nameof(sale),
                    "Sale data is required."
                );
            }


            if (employeeId <= 0)
            {
                throw new ArgumentException(
                    "Employee id is invalid."
                );
            }


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


            if (sale.AdditionalCharges < 0)
            {
                throw new ArgumentException(
                    "Additional charges cannot be negative."
                );
            }
        }


        // ===================================================
        // SALE ITEM VALIDATION
        // ===================================================

        private void ValidateSaleItem(
            SaleItem saleItem)
        {
            if (saleItem == null)
            {
                throw new ArgumentException(
                    "Sale item cannot be null."
                );
            }


            if (saleItem.ItemId <= 0)
            {
                throw new ArgumentException(
                    "Item id is invalid."
                );
            }


            if (saleItem.OrderedQuantity <= 0)
            {
                throw new ArgumentException(
                    "Ordered quantity must be greater than zero."
                );
            }
        }


        // ===================================================
        // BATCH ALLOCATION
        // ===================================================

        private void AllocateBatches(
            SaleItem saleItem, int employeeId)
        {
            /*
             * Repository returns usable batches
             * ordered by earliest expiry first.
             *
             * FEFO:
             * First Expire, First Out
             */
            var batches =
                _repository.GetAvailableBatchesForItem(
                    saleItem.ItemId
                );


            int quantityRemaining =
                saleItem.OrderedQuantity;


            foreach (var batch in batches)
            {
                /*
                 * How much from this batch has
                 * already been sold?
                 */
                int alreadyAllocated =
                    _repository
                        .GetAllocatedQuantityForBatch(
                            batch.BatchId
                        );


                /*
                 * Example:
                 *
                 * ReceivedQuantity = 100
                 * AlreadyAllocated = 70
                 *
                 * Available = 30
                 */
                int availableQuantity =
                    batch.ReceivedQuantity -
                    alreadyAllocated;


                if (availableQuantity <= 0)
                {
                    continue;
                }


                /*
                 * Take whichever is smaller:
                 *
                 * quantity still needed
                 * OR
                 * quantity available in batch.
                 */
                int quantityToAllocate =
                    Math.Min(
                        quantityRemaining,
                        availableQuantity
                    );


                var allocation =
                    new BatchAllocation
                    {
                        SaleItemId =
                            saleItem.SaleItemId,

                        BatchId =
                            batch.BatchId,

                        AllocatedQuantity =
                            quantityToAllocate,

                        CreatedAt =
                            DateTime.Now
                    };


                _repository.AddBatchAllocation(
                    allocation
                );
                var movement =
    new InventoryMovement
    {
        BatchId =
            batch.BatchId,

        MovementType =
            InventoryMovementType.SaleOut,

        QuantityChange =
            -quantityToAllocate,

        ReferenceId =
            saleItem.SaleItemId,

        Remarks =
            "Stock sold.",

        MovementDate =
            DateTime.Now,

        PerformedByEmployeeId = employeeId
            
    };

                _repository.AddInventoryMovement(
                    movement
                );


                quantityRemaining -=
                    quantityToAllocate;


                /*
                 * SaleItem has now been completely
                 * fulfilled.
                 */
                if (quantityRemaining == 0)
                {
                    break;
                }
            }


            /*
             * We checked every usable batch,
             * but still couldn't fulfill the order.
             */
            if (quantityRemaining > 0)
            {
                throw new InvalidOperationException(
                    $"Insufficient stock for item {saleItem.ItemId}."
                );
            }
        }
    }
}