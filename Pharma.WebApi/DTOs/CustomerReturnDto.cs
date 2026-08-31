using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;

namespace PharmacyMangementSystem.DTOs.CustomerReturn
{
    public class CustomerReturnDto
    {
        public int CustomerReturnId { get; set; }

        public int SaleId { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Remarks { get; set; }

        public CustomerReturnStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<CustomerReturnItemDto> Items { get; set; }
    }
}