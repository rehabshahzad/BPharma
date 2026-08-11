using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }

        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        //disocunt is only added at the end , no per item discount is added


    }
}
