using Pharma.Entity.Enums;
using System;

namespace PharmacyMangementSystem.DTOs
{
    public class UpdateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }

        public EmployeeRole Role { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string Username { get; set; }

       
    }
}