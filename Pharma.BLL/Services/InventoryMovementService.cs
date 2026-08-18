using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class InventoryMovementService
        : IInventoryMovementService
    {
        private readonly IInventoryMovementRepository _repository;

        public InventoryMovementService(
            IInventoryMovementRepository repository)
        {
            _repository = repository;
        }

        public List<InventoryMovement> GetAllMovements()
        {
            return _repository.GetAllMovements();
        }



        public InventoryMovement GetMovementById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Inventory movement id is invalid."
                );
            }

            var movement =
                _repository.GetMovementById(id);

            if (movement == null)
            {
                throw new KeyNotFoundException(
                    "Inventory movement does not exist."
                );
            }

            return movement;
        }


        public List<InventoryMovement> GetMovementsByBatchId(
            int batchId)
        {
            if (batchId <= 0)
            {
                throw new ArgumentException(
                    "Batch id is invalid."
                );
            }

            var batch =
                _repository.GetBatchById(batchId);

            if (batch == null)
            {
                throw new KeyNotFoundException(
                    "Batch does not exist."
                );
            }

            return _repository
                .GetMovementsByBatchId(batchId);
        }


        public InventoryMovement CreateMovement(
            int batchId,
            InventoryMovementType movementType,
            int quantity,
            int? referenceId,
            string remarks,
            int employeeId)
        {
            if (batchId <= 0)
            {
                throw new ArgumentException(
                    "Batch id is invalid."
                );
            }

            var batch =
                _repository.GetBatchById(batchId);

            if (batch == null)
            {
                throw new KeyNotFoundException(
                    "Batch does not exist."
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


            if (quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero."
                );
            }


            int quantityChange =
                GetSignedQuantity(
                    movementType,
                    quantity
                );
            var currentStock =
    _repository.GetCurrentStockForBatch(
        batchId
    );

            if (movementType == InventoryMovementType.DamagedOut ||
                movementType == InventoryMovementType.ExpiredOut ||
                movementType == InventoryMovementType.AdjustmentOut)
            {
                if (quantity > currentStock)
                {
                    throw new InvalidOperationException(
                        "Quantity exceeds available stock."
                    );
                }
            }

            var movement =
                new InventoryMovement
                {
                    BatchId = batchId,

                    MovementType =
                        movementType,

                    QuantityChange =
                        quantityChange,

                    ReferenceId =
                        referenceId,

                    Remarks =
                        remarks?.Trim(),

                    MovementDate =
                        DateTime.Now,

                    PerformedByEmployeeId =
                        employeeId
                };


            _repository.AddMovement(
                movement
            );

            _repository.SaveChanges();

            return movement;
        }


        private int GetSignedQuantity(
            InventoryMovementType movementType,
            int quantity)
        {
            switch (movementType)
            {
                case InventoryMovementType.PurchaseReceived:

                case InventoryMovementType.CustomerReturnIn:

                case InventoryMovementType.AdjustmentIn:

                    return quantity;


                case InventoryMovementType.SaleOut:

                case InventoryMovementType.SupplierReturnOut:

                case InventoryMovementType.ExpiredOut:

                case InventoryMovementType.DamagedOut:

                case InventoryMovementType.AdjustmentOut:

                    return -quantity;


                default:

                    throw new ArgumentException(
                        "Invalid inventory movement type."
                    );
            }
        }
    }
}