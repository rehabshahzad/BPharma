using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly ICategoryService _service;

        // Temporary until authentication/JWT is implemented.
        private const int CurrentEmployeeId = 1;


        public CategoryController()
        {
            _context = new PharmacyDbContext();

            ICategoryRepository categoryRepository =
                new CategoryRepository(_context);

            _service =
                new CategoryService(categoryRepository);
        }


        // GET: api/categories
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllCategories()
        {
            var categories = _service
                .GetAllCategories()
                .Select(c => MapToResponse(c))
                .ToList();

            return Ok(categories);
        }


        // GET: api/categories/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCategoryById(int id)
        {
            try
            {
                var category =
                    _service.GetCategoryById(id);

                return Ok(
                    MapToResponse(category)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        // POST: api/categories
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(
            CreateCategoryDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Category data is required."
                );
            }

            try
            {
                var category = new Category
                {
                    CategoryName =
                        request.CategoryName,

                    Description =
                        request.Description
                };

                var createdCategory =
                    _service.CreateCategory(
                        category,
                        CurrentEmployeeId
                    );

                return Content(
                    HttpStatusCode.Created,
                    MapToResponse(createdCategory)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
        }


        // PUT: api/categories/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(
            int id,
            UpdateCategoryDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Category data is required."
                );
            }

            try
            {
                var updatedCategory = new Category
                {
                    CategoryName =
                        request.CategoryName,

                    Description =
                        request.CategoryDescription,

                    isActive =
                        request.isActive
                };

                var category =
                    _service.UpdateCategory(
                        id,
                        updatedCategory,
                        CurrentEmployeeId
                    );

                return Ok(
                    MapToResponse(category)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
        }


        // Category Entity -> CategoryResponseDto
        private CategoryResponseDto MapToResponse(
            Category c)
        {
            return new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description,
                isActive = c.isActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            };
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}