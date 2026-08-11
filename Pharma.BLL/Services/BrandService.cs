using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;


        public BrandService(
            IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }


        public Brand GetBrandById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid brand ID."
                );
            }

            var brand =
                _brandRepository.GetBrandById(id);

            if (brand == null)
            {
                throw new KeyNotFoundException(
                    "Brand not found."
                );
            }

            return brand;
        }


        public List<Brand> GetAllBrands()
        {
            return _brandRepository.GetAllBrands();
        }


        public Brand CreateBrand(
            Brand brand,
            int createdByEmployeeId)
        {
            ValidateBrand(brand);

            if (createdByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Invalid creator employee ID."
                );
            }

            brand.BrandName =
                brand.BrandName.Trim();

            if (_brandRepository.BrandExists(
                    brand.BrandName))
            {
                throw new InvalidOperationException(
                    "Brand already exists."
                );
            }

            brand.IsActive = true;

            brand.CreatedByEmployeeId =
                createdByEmployeeId;

            brand.CreatedAt =
                DateTime.Now;

            brand.UpdatedByEmployeeId =
                null;

            brand.UpdatedAt =
                null;

            _brandRepository.AddBrand(brand);

            _brandRepository.SaveChanges();

            return brand;
        }


        public Brand UpdateBrand(
            int id,
            Brand updatedBrand,
            int updatedByEmployeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid brand ID."
                );
            }

            if (updatedByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Invalid updater employee ID."
                );
            }

            ValidateBrand(updatedBrand);

            var existingBrand =
                _brandRepository.GetBrandById(id);

            if (existingBrand == null)
            {
                throw new KeyNotFoundException(
                    "Brand not found."
                );
            }

            updatedBrand.BrandName =
                updatedBrand.BrandName.Trim();

            if (_brandRepository.BrandExists(
                    updatedBrand.BrandName,
                    id))
            {
                throw new InvalidOperationException(
                    "Brand already exists."
                );
            }

            existingBrand.BrandName =
                updatedBrand.BrandName;

            existingBrand.IsActive =
                updatedBrand.IsActive;

            existingBrand.UpdatedByEmployeeId =
                updatedByEmployeeId;

            existingBrand.UpdatedAt =
                DateTime.Now;

            _brandRepository.SaveChanges();

            return existingBrand;
        }


        private void ValidateBrand(Brand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException(
                    nameof(brand),
                    "Brand data is required."
                );
            }

            if (string.IsNullOrWhiteSpace(
                    brand.BrandName))
            {
                throw new ArgumentException(
                    "Brand name is required."
                );
            }

            if (brand.BrandName.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Brand name should not exceed 100 characters."
                );
            }
        }
    }
}