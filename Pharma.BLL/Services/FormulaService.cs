using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class FormulaService : IFormulaService
    {
        private readonly IFormulaRepository _formulaRepository;


        public FormulaService(
            IFormulaRepository formulaRepository)
        {
            _formulaRepository = formulaRepository;
        }


        public Formula GetFormulaById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid formula ID."
                );
            }

            var formula =
                _formulaRepository.GetFormulaById(id);

            if (formula == null)
            {
                throw new KeyNotFoundException(
                    "Formula not found."
                );
            }

            return formula;
        }


        public List<Formula> GetAllFormulas()
        {
            return _formulaRepository.GetAllFormulas();
        }


        public Formula CreateFormula(
            Formula formula,
            int createdByEmployeeId)
        {
            ValidateFormula(formula);

            if (createdByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Created-by employee ID is required."
                );
            }

            formula.FormulaName =
                formula.FormulaName.Trim();

            if (_formulaRepository.FormulaExists(
                    formula.FormulaName))
            {
                throw new InvalidOperationException(
                    "Formula already exists."
                );
            }

            formula.isActive = true;

            formula.CreatedByEmployeeId =
                createdByEmployeeId;

            formula.CreatedAt =
                DateTime.Now;

            formula.UpdatedByEmployeeId =
                null;

            formula.UpdatedAt =
                null;

            _formulaRepository.AddFormula(formula);

            _formulaRepository.SaveChanges();

            return formula;
        }


        public Formula UpdateFormula(
            int id,
            Formula updatedFormula,
            int updatedByEmployeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid formula ID."
                );
            }

            if (updatedByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Updated-by employee ID is required."
                );
            }

            ValidateFormula(updatedFormula);

            var existingFormula =
                _formulaRepository.GetFormulaById(id);

            if (existingFormula == null)
            {
                throw new KeyNotFoundException(
                    "Formula not found."
                );
            }

            updatedFormula.FormulaName =
                updatedFormula.FormulaName.Trim();

            if (_formulaRepository.FormulaExists(
                    updatedFormula.FormulaName,
                    id))
            {
                throw new InvalidOperationException(
                    "Formula already exists."
                );
            }

            existingFormula.FormulaName =
                updatedFormula.FormulaName;

            existingFormula.isActive =
                updatedFormula.isActive;

            existingFormula.UpdatedByEmployeeId =
                updatedByEmployeeId;

            existingFormula.UpdatedAt =
                DateTime.Now;

            _formulaRepository.SaveChanges();

            return existingFormula;
        }


        private void ValidateFormula(Formula formula)
        {
            if (formula == null)
            {
                throw new ArgumentNullException(
                    nameof(formula),
                    "Formula data is required."
                );
            }

            if (string.IsNullOrWhiteSpace(
                    formula.FormulaName))
            {
                throw new ArgumentException(
                    "Formula name is required."
                );
            }

            if (formula.FormulaName.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Formula name should not exceed 100 characters."
                );
            }
        }
    }
}