using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Enums
{
    public enum  InventoryMovementType
    {
        PurchaseReceived = 1,
        Sale = 2,
        CustomerReturn = 3,
        SupplierReturn = 4,
        Expired = 5,
        Damaged = 6,
        //manual entry corrections
        AdjustmentIn = 7,  //system shows low stock but in reality more stock available
        AdjustmentOut = 8 //system shows more stock but physically less stock available
    }
}
