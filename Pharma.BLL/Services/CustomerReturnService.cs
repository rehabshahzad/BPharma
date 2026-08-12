using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class CustomerReturnService : ICustomerReturnService
    {
        private readonly ICustomerReturnRepository _repository;

        public CustomerReturnService(
            ICustomerReturnRepository repository)
        {
            _repository = repository;
        }


        public List<CustomerReturn> GetAllCustomerReturns()
        {
            return _repository.GetAllCustomerReturns();
        }


        public CustomerReturn GetCustomerReturnById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Customer return id is invalid."
                );
            }


            var customerReturn =
                _repository.GetCustomerReturnById(id);


            if (customerReturn == null)
            {
                throw new KeyNotFoundException(
                    "Customer return does not exist."
                );
            }


            return customerReturn;
        }


        public CustomerReturn CreateCustomerReturn(
            CustomerReturn customerReturn,
            List<CustomerReturnItem> items,
            int employeeId)
        {
            if (customerReturn == null)
            {
                throw new ArgumentNullException(
                    nameof(customerReturn),
                    "Customer return data is required."
                );
            }


            if (employeeId <= 0)
            {
                throw new ArgumentException(
                    "Employee id is invalid."
                );
            }


            if (customerReturn.SaleId <= 0)
            {
                throw new ArgumentException(
                    "Sale id is invalid."
                );
            }


            if (items == null || items.Count == 0)
            {
                throw new ArgumentException(
                    "At least one returned item is required."
                );
            }


            var sale =
                _repository.GetSaleById(
                    customerReturn.SaleId
                );


            if (sale == null)
            {
                throw new KeyNotFoundException(
                    "Sale does not exist."
                );
            }


            customerReturn.Remarks =
                customerReturn.Remarks?.Trim();

            customerReturn.ReceivedByEmployeeId =
                employeeId;

            customerReturn.ReturnDate =
                DateTime.Now;

            customerReturn.CreatedAt =
                DateTime.Now;


            _repository.AddReturn(
                customerReturn
            );

            _repository.SaveChanges();


            foreach (var item in items)
            {
                ValidateReturnItem(
                    item,
                    customerReturn.SaleId
                );


                var saleItem =
                    _repository.GetSaleItemById(
                        item.SaleItemId
                    );


                // Refund amount calculated by backend
                item.RefundAmount =
                    item.ReturnQuantity *
                    saleItem.UnitPrice;


                item.CustomerReturnId =
                    customerReturn.CustomerReturnId;


                item.Reason =
                    item.Reason?.Trim();


                _repository.AddReturnItem(item);
            }


            _repository.SaveChanges();

            return customerReturn;
        }


        public CustomerReturn UpdateCustomerReturn(
            int id,
            CustomerReturn customerReturn,
            int employeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Customer return id is invalid."
                );
            }


            if (employeeId <= 0)
            {
                throw new ArgumentException(
                    "Employee id is invalid."
                );
            }


            if (customerReturn == null)
            {
                throw new ArgumentNullException(
                    nameof(customerReturn),
                    "Customer return data is required."
                );
            }


            var existing =
                _repository.GetCustomerReturnById(id);


            if (existing == null)
            {
                throw new KeyNotFoundException(
                    "Customer return does not exist."
                );
            }


            existing.Remarks =
                customerReturn.Remarks?.Trim();

            existing.Status =
                customerReturn.Status;

            existing.UpdatedByEmployeeId =
                employeeId;

            existing.UpdatedAt =
                DateTime.Now;


            _repository.SaveChanges();

            return existing;
        }


        private void ValidateReturnItem(
            CustomerReturnItem item,
            int saleId)
        {
            if (item == null)
            {
                throw new ArgumentException(
                    "Customer return item is required."
                );
            }


            if (item.SaleItemId <= 0)
            {
                throw new ArgumentException(
                    "Sale item id is invalid."
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


            var saleItem =
                _repository.GetSaleItemById(
                    item.SaleItemId
                );


            if (saleItem == null)
            {
                throw new KeyNotFoundException(
                    "Sale item does not exist."
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


            if (!_repository.SaleItemBelongsToSale(
                item.SaleItemId,
                saleId))
            {
                throw new InvalidOperationException(
                    "Sale item does not belong to this sale."
                );
            }


            if (!_repository.BatchWasUsedForSaleItem(
                item.BatchId,
                item.SaleItemId))
            {
                throw new InvalidOperationException(
                    "This batch was not used for this sale item."
                );
            }


            var batchAllocation =
                _repository.GetBatchAllocation(
                    item.BatchId,
                    item.SaleItemId
                );


            if (batchAllocation == null)
            {
                throw new KeyNotFoundException(
                    "Batch allocation does not exist."
                );
            }


            var alreadyReturned =
                _repository.GetAlreadyReturnedQuantity(
                    item.SaleItemId,
                    item.BatchId
                );


            var remainingReturnable =
                batchAllocation.AllocatedQuantity -
                alreadyReturned;


            if (remainingReturnable <= 0)
            {
                throw new InvalidOperationException(
                    "No quantity remains available for return."
                );
            }


            if (item.ReturnQuantity >
                remainingReturnable)
            {
                throw new InvalidOperationException(
                    "Return quantity exceeds the remaining returnable quantity."
                );
            }
        }
    }
}