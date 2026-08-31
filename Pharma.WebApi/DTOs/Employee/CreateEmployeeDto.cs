using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs
{
    public class CreateEmployeeDto //takes that data from an employee thats needed to create it
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }

        public EmployeeRole Role { get; set; }

        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }

        public string Username { get; set; }

        //Plain temporary password received from Postman/frontend.
        public string TemporaryPassword { get; set; }

    //No created by fields. should auto detect by login
    }
}