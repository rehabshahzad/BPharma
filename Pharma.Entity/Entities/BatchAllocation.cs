using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class BatchAllocation
    {
        public int BatchAllocationId { get; set; }

        public int SaleItemId { get; set; }
        public virtual SaleItem SaleItem { get; set; }

        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }

        public int AllocatedQuantity { get; set; }
        public DateTime CreatedAt { get; set; } //employee already known by sale-item -> sale > sold by employeeid
    }
}
