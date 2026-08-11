using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class CustomerReturnItem
    {
        public int CustomerReturnItemId { get; set; }

        public int CustomerReturnId { get; set; }
        public virtual CustomerReturn CustomerReturn { get; set; }

        public int SaleItemId { get; set; }
        public virtual SaleItem SaleItem { get; set; }

        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }

        public int ReturnQuantity { get; set; }

        public decimal RefundAmount { get; set; }

        public string Reason { get; set; }
   


        public bool CanReturnToStock { get; set; } //is returned item ok to put back into stock
    }
}
