using System;

namespace PharmacyMangementSystem.DTOs.Formula
{
    public class FormulaDto
    {
        public int FormulaId { get; set; }

        public string FormulaName { get; set; }

        public bool isActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}