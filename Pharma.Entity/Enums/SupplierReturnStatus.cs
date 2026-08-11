using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Enums
{
    public enum SupplierReturnStatus
    {
        Draft = 1,
        Approved = 2,
        Dispatched = 3,
        Completed = 4,
        Rejected = 5,
        Cancelled=6
    }
}
