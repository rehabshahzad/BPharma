using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [Authorize]
    [RoutePrefix("api/brands")]
    public class BrandController : ApiController
    {
        
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }


        // GET: api/brands
        [Authorize(Roles = "Admin, Pharmacist, InventoryManager")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllBrands()
        {
            var brands = _service
                .GetAllBrands()
                .Select(b => MapToDto(b))
                .ToList();

            return Ok(brands);
        }


        // GET: api/brands/1
        [HttpGet]
        [Authorize(Roles = "Admin, Pharmacist, InventoryManager")]
        [Route("{id:int}")]
        public IHttpActionResult GetBrandById(int id)
        {
            try
            {
                var brand =
                    _service.GetBrandById(id);

                return Ok(
                    MapToDto(brand)
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


        // POST: api/brands
        [HttpPost]
        [Authorize(Roles = "Admin,InventoryManager")]
        [Route("")]
        public IHttpActionResult CreateBrand(
            BrandDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Brand data is required."
                );
            }

            try
            {
                var brand = new Brand
                {
                    BrandName = request.BrandName
                };

                var createdBrand =
                    _service.CreateBrand(
                        brand,
                        GetCurrentEmployeeId()
                    );

                return Content(
                    HttpStatusCode.Created,
                    MapToDto(createdBrand)
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


        // PUT: api/brands/1
        [HttpPut]
        [Authorize(Roles = "Admin,InventoryManager")]
        [Route("{id:int}")]
        public IHttpActionResult UpdateBrand(
            int id,
            BrandDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Brand data is required."
                );
            }

            try
            {
                var updatedBrand = new Brand
                {
                    BrandName = request.BrandName,
                    IsActive = request.IsActive
                };

                var brand =
                    _service.UpdateBrand(
                        id,
                        updatedBrand,
                        GetCurrentEmployeeId()
                    );

                return Ok(
                    MapToDto(brand)
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


        // Brand Entity -> BrandDto
        private BrandDto MapToDto(Brand brand)
        {
            return new BrandDto
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                IsActive = brand.IsActive,
                CreatedAt = brand.CreatedAt,
                UpdatedAt = brand.UpdatedAt
            };
        }
        private int GetCurrentEmployeeId()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claim = identity?.FindFirst("EmployeeId");

            if (claim == null)
            {
                throw new UnauthorizedAccessException(
                    "Employee ID claim not found."
                );
            }

            return int.Parse(claim.Value);
        }



    }
}