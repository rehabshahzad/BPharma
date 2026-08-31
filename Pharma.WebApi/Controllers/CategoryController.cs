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
using System.Security.Claims;

namespace PharmacyMangementSystem.Controllers
{
    [Authorize]
    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
       
        private readonly ICategoryService _service;

        
   
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }


        // GET: api/categories
        [HttpGet]
        [Authorize(Roles = "Admin, Pharmacist, InventoryManager")]
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
        [Authorize(Roles = "Admin, Pharmacist, InventoryManager")]
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
        [Authorize(Roles = "Admin,InventoryManager")]
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
                        GetCurrentEmployeeId()
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
        [Authorize(Roles = "Admin, InventoryManager")]
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
                        GetCurrentEmployeeId()
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
       
        private int GetCurrentEmployeeId()
        {
            var identity = User.Identity as ClaimsIdentity;
            var claim = identity?.FindFirst("EmployeeId");
            if(claim == null)
            {
                throw new UnauthorizedAccessException("Employee ID claim ot found");
            }
            return int.Parse(claim.Value);
        }
       
    }
}