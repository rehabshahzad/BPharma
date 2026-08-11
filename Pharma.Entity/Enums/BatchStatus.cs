using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Enums
{
    public enum BatchStatus
    {
        Available = 1,
        Exhausted = 2, //no stock
        Expired = 3,
        Quarantined = 4 //temporarily blocked
                        //return and disposed in inventory movements
    }
}
