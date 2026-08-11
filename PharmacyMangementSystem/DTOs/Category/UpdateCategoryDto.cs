using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMangementSystem.DTOs.Category
{
    public class UpdateCategoryDto

    {  
        public string CategoryName{ get; set; }
        public string CategoryDescription{ get; set; }
        public bool isActive { get; set; }

    }
}