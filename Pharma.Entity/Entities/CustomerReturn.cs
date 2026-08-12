using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharma.Entity.Enums;
namespace Pharma.Entity.Entities
{
   public  class CustomerReturn
    {
        public int CustomerReturnId { get; set; }
        public virtual ICollection<CustomerReturnItem>
    CustomerReturnItems
        { get; set; }
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Remarks { get; set; }
          
        public CustomerReturnStatus Status { get; set; }
        public int ReceivedByEmployeeId { get; set; }
        public virtual Employee ReceivedByEmployee { get; set; }

        public int? UpdatedByEmployeeId { get; set; }
        public virtual Employee UpdatedByEmployee { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt {  get; set; }
    }
}
