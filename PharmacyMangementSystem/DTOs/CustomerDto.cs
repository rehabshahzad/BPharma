using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }

       

    }
}