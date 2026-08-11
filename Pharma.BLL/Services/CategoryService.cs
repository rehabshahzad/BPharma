using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        // GET CATEGORY BY ID
        public Category GetCategoryById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid category ID."
                );
            }

            var category =
                _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                throw new KeyNotFoundException(
                    "Category not found."
                );
            }

            return category;
        }


        // GET ALL CATEGORIES
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }


        // CREATE CATEGORY
        public Category CreateCategory(
            Category cat,
            int createdByEmployeeId)
        {
            ValidateCategory(cat);

            if (createdByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Created-by employee ID is required."
                );
            }

            // Trim before duplicate checking
            cat.CategoryName = cat.CategoryName.Trim();
            cat.Description = cat.Description.Trim();

            if (_categoryRepository.CategoryExists(
                    cat.CategoryName))
            {
                throw new InvalidOperationException(
                    "Category already exists."
                );
            }

            cat.isActive = true;

            cat.CreatedByEmployeeId =
                createdByEmployeeId;

            cat.CreatedAt = DateTime.Now;

            cat.UpdatedByEmployeeId = null;
            cat.UpdatedAt = null;

            _categoryRepository.AddCategory(cat);

            _categoryRepository.SaveChanges();

            return cat;
        }


        // UPDATE CATEGORY
        public Category UpdateCategory(
            int id,
            Category updatedCategory,
            int updatedByEmployeeId)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Enter a valid category ID."
                );
            }

            if (updatedByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Updated-by employee ID is required."
                );
            }

            ValidateCategory(updatedCategory);

            var existingCategory =
                _categoryRepository.GetCategoryById(id);

            if (existingCategory == null)
            {
                throw new KeyNotFoundException(
                    "Category not found."
                );
            }

            updatedCategory.CategoryName =
                updatedCategory.CategoryName.Trim();

            updatedCategory.Description =
                updatedCategory.Description.Trim();

            // Ignore the category currently being updated
            if (_categoryRepository.CategoryExists(
                    updatedCategory.CategoryName,
                    id))
            {
                throw new InvalidOperationException(
                    "Category already exists."
                );
            }

            // Modify the TRACKED entity
            existingCategory.CategoryName =
                updatedCategory.CategoryName;

            existingCategory.Description =
                updatedCategory.Description;

            existingCategory.isActive =
                updatedCategory.isActive;

            existingCategory.UpdatedByEmployeeId =
                updatedByEmployeeId;

            existingCategory.UpdatedAt =
                DateTime.Now;

            _categoryRepository.SaveChanges();

            return existingCategory;
        }


        // COMMON CATEGORY VALIDATION
        private void ValidateCategory(Category cat)
        {
            if (cat == null)
            {
                throw new ArgumentNullException(
                    nameof(cat),
                    "Category data is required."
                );
            }

            if (string.IsNullOrWhiteSpace(
                    cat.CategoryName))
            {
                throw new ArgumentException(
                    "Category name is required."
                );
            }

            if (cat.CategoryName.Trim().Length > 50)
            {
                throw new ArgumentException(
                    "Category name should not exceed 50 characters."
                );
            }

            if (string.IsNullOrWhiteSpace(
                    cat.Description))
            {
                throw new ArgumentException(
                    "Description is required."
                );
            }

            if (cat.Description.Trim().Length > 255)
            {
                throw new ArgumentException(
                    "Description should not exceed 255 characters."
                );
            }
        }
    }
}