using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class SupplierReturnItem
    {
        public int SupplierReturnItemId { get; set; }

        public int SupplierReturnId { get; set; }
        public virtual SupplierReturn SupplierReturn { get; set; }

        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }

        public int ReturnQuantity { get; set; }

        public decimal ReturnAmount { get; set; }

        public string Reason { get; set; }

    }
}
