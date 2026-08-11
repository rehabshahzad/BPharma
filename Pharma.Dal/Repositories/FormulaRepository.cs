using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pharma.Dal.Repositories
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly PharmacyDbContext _context;

        public FormulaRepository(PharmacyDbContext context)
        {
            _context = context;
        }


        public Formula GetFormulaById(int id)
        {
            return _context.Formulas
                .FirstOrDefault(f => f.FormulaId == id);
        }


        public List<Formula> GetAllFormulas()
        {
            return _context.Formulas
                .AsNoTracking()
                .ToList();
        }


        public void AddFormula(Formula formula)
        {
            _context.Formulas.Add(formula);
        }


        public bool FormulaExists(
            string formulaName,
            int? excludeFormulaId = null)
        {
            return _context.Formulas.Any(f =>
                f.FormulaName == formulaName &&
                (!excludeFormulaId.HasValue ||
                 f.FormulaId != excludeFormulaId.Value)
            );
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}