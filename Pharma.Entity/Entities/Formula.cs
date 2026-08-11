using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Entity.Entities
{
public class Formula
    {
        public int FormulaId { get; set; }
        public string FormulaName { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedByEmployeeId { get; set; }
        public virtual Employee CreatedByEmployee { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedByEmployeeId {  get; set; }
        public virtual Employee UpdatedByEmployee { get; set; }
        

    }
}
