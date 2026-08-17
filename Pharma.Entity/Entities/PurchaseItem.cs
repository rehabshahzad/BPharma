using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class PurchaseItem
    {
        public int PurchaseItemId { get; set; }

        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int OrderedQuantity { get; set; }

        public decimal UnitPurchasePrice { get; set; }  //unitPprice *batch.receievedQuantity will give me the total id have to pay
        

    }
}
