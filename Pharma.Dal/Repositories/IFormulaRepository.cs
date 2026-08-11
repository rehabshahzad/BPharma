using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.Dal.Repositories
{
    public interface IFormulaRepository
    {
        Formula GetFormulaById(int id);

        List<Formula> GetAllFormulas();

        void AddFormula(Formula formula);

        bool FormulaExists(
            string formulaName,
            int? excludeFormulaId = null
        );

        void SaveChanges();
    }
}