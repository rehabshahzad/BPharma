using Pharma.Entity.Enums;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public DateTime SaleDate { get; set; }

        //Total before sale-level discount.
        public decimal SubtotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        //Final amount paid by the customer.
        public decimal TotalAmount { get; set; }

        public SaleStatus Status { get; set; }

        public int SoldByEmployeeId { get; set; }

        //Pharmacist or employee who processed the sale.
        public virtual Employee SoldByEmployee { get; set; }

        public string Notes { get; set; }
        public DateTime SoldAt { get; set; }
        public int? UpdatedByEmployeeId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Employee UpdatedByEmployee { get; set; }
    }
}
