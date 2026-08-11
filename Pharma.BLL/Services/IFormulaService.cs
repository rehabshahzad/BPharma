using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface IFormulaService
    {
        Formula GetFormulaById(int id);

        List<Formula> GetAllFormulas();

        Formula CreateFormula(
            Formula formula,
            int createdByEmployeeId
        );

        Formula UpdateFormula(
            int id,
            Formula formula,
            int updatedByEmployeeId
        );
    }
}