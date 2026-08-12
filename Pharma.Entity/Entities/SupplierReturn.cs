using PharmacyManagement.Entity.Entities;
using System;
using Pharma.Entity.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class SupplierReturn
    {
        public int SupplierReturnId { get; set; }

     

        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; } //supplier can be accessed thru purchase

        public DateTime ReturnDate { get; set; }
        public virtual ICollection<SupplierReturnItem>
     SupplierReturnItems
        { get; set; }

        public string Reason { get; set; }

        public SupplierReturnStatus Status { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public virtual Employee CreatedByEmployee { get; set; }

        public int? UpdatedByEmployeeId{ get; set; }
        public virtual Employee UpdatedByEmployee { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
