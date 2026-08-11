using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/brands")]
    public class BrandController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly IBrandService _service;

        // TEMPORARY until JWT/Auth is implemented
        private const int CurrentEmployeeId = 1;


        public BrandController()
        {
            _context = new PharmacyDbContext();

            IBrandRepository brandRepository =
                new BrandRepository(_context);

            _service =
                new BrandService(brandRepository);
        }


        // GET: api/brands
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
                        CurrentEmployeeId
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
                        CurrentEmployeeId
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