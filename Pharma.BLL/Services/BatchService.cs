using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _repository;

        public BatchService(IBatchRepository repository)
        {
            _repository = repository;
        }


        public List<Batch> GetAllBatches()
        {
            return _repository.GetAllBatches();
        }


        public Batch GetBatchById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Batch id is invalid."
                );
            }

            var batch =
                _repository.GetBatchById(id);

            if (batch == null)
            {
                throw new KeyNotFoundException(
                    "Batch does not exist."
                );
            }

            return batch;
        }


        public Batch CreateBatch(
            Batch batch,
            int employeeId)
        {
            ValidateBatch(
                batch,
                employeeId
            );


            var purchaseItem =
                _repository.GetPurchaseItemById(
                    batch.PurchaseItemId
                );

            if (purchaseItem == null)
            {
                throw new KeyNotFoundException(
                    "Purchase item does not exist."
                );
            }


            if (_repository.BatchNumberExists(
                batch.BatchNumber))
            {
                throw new InvalidOperationException(
                    "Batch number already exists."
                );
            }


            var alreadyReceived =
                _repository.GetTotalReceivedQuantity(
                    batch.PurchaseItemId
                );


            var remainingReceivable =
                purchaseItem.OrderedQuantity -
                alreadyReceived;


            if (remainingReceivable <= 0)
            {
                throw new InvalidOperationException(
                    "This purchase item has already been fully received."
                );
            }


            if (batch.ReceivedQuantity >
                remainingReceivable)
            {
                throw new InvalidOperationException(
                    "Received quantity exceeds the remaining ordered quantity."
                );
            }


            batch.BatchNumber =
                batch.BatchNumber.Trim();

            batch.Status =
                BatchStatus.Available;

            batch.ReceivedDate =
                DateTime.Now;

            batch.CreatedAt =
                DateTime.Now;

            batch.CreatedByEmployeeId =
                employeeId;


            _repository.AddBatch(batch);

            _repository.SaveChanges();

            return batch;
        }


        public Batch UpdateBatch(
            int id,
            Batch batch,
            int employeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Batch id is invalid."
                );
            }


            ValidateBatch(
                batch,
                employeeId
            );


            var existingBatch =
                _repository.GetBatchById(id);

            if (existingBatch == null)
            {
                throw new KeyNotFoundException(
                    "Batch does not exist."
                );
            }


            if (_repository.BatchNumberExists(
                batch.BatchNumber,
                id))
            {
                throw new InvalidOperationException(
                    "Batch number already exists."
                );
            }


            existingBatch.BatchNumber =
                batch.BatchNumber.Trim();

            existingBatch.ManufacturingDate =
                batch.ManufacturingDate;

            existingBatch.ExpiryDate =
                batch.ExpiryDate;

            existingBatch.Status =
                batch.Status;

            existingBatch.UpdatedByEmployeeId =
                employeeId;

            existingBatch.UpdatedAt =
                DateTime.Now;


            _repository.SaveChanges();

            return existingBatch;
        }


        private void ValidateBatch(
            Batch batch,
            int employeeId)
        {
            if (batch == null)
            {
                throw new ArgumentNullException(
                    nameof(batch),
                    "Batch data is required."
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


            if (batch.PurchaseItemId <= 0)
            {
                throw new ArgumentException(
                    "Purchase item id is invalid."
                );
            }


            if (string.IsNullOrWhiteSpace(
                batch.BatchNumber))
            {
                throw new ArgumentException(
                    "Batch number is required."
                );
            }


            if (batch.ReceivedQuantity <= 0)
            {
                throw new ArgumentException(
                    "Received quantity must be greater than zero."
                );
            }


            if (batch.ExpiryDate <= DateTime.Now)
            {
                throw new ArgumentException(
                    "Expiry date must be in the future."
                );
            }


            if (batch.ManufacturingDate.HasValue &&
                batch.ManufacturingDate.Value >
                batch.ExpiryDate)
            {
                throw new ArgumentException(
                    "Manufacturing date cannot be after expiry date."
                );
            }
        }
    }
}