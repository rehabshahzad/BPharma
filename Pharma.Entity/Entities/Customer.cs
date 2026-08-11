using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Contact {  get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int? CreatedByEmployeeId { get; set; }
        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedByEmployeeId { get; set; } //nullable cuz a newly created employee may not have been updated
        public virtual Employee UpdatedByEmployee { get; set; } // navigation property for employee who last updated details

        public DateTime UpdatedAt { get; set; }


    }
}
