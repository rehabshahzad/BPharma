using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs
{
    public class BrandDto
    { //using one common dto and ater adding validations so data thats not needed isn't added
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}