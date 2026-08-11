using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.DAL.Repositories;
using PharmacyManagement.Entity.Entities;
using PharmacyMangementSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly IEmployeeService _employeeService;
     

        // TEMPORARY until authentication/JWT is implemented.
        // We are pretending EmployeeId 1 is currently logged in.
        private const int CurrentEmployeeId = 1;


        public EmployeesController()
        {
            _context = new PharmacyDbContext();

            IEmployeeRepository employeeRepository =
                new EmployeeRepository(_context);

            _employeeService =
                new EmployeeService(employeeRepository);
        }


        // GET: api/employees
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllEmployees()
        {
            var employees = _employeeService
                .GetAllEmployees()
                .Select(e => MapToResponse(e))
                .ToList();

            return Ok(employees);
        }


        // GET: api/employees/:id
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var employee =
                    _employeeService.GetEmployeeById(id);

                return Ok(MapToResponse(employee));
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


        // POST: api/employees
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(CreateEmployeeDto request)
        {
            if (request == null)
                return BadRequest("Employee data is required.");

            try
            {
                var employee = new Employee
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    Contact = request.Contact,
                    Email = request.Email,
                    Role = request.Role,
                    StartDate = request.StartDate,
                    Salary = request.Salary,
                    Username = request.Username
                };

                var createdEmployee =
                    _employeeService.CreateEmployee(
                        employee,
                        request.TemporaryPassword,
                        CurrentEmployeeId
                    );


                return Content(
                    HttpStatusCode.Created,
                    MapToResponse(createdEmployee)
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

        // PUT: api/employees/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(
            int id,
            UpdateEmployeeDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Employee data is required."
                );
            }

            try
            {
                var updatedEmployee = new Employee
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    Contact = request.Contact,
                    Email = request.Email,
                    Role = request.Role,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Salary = request.Salary,
                    IsActive = request.IsActive,
                    Username = request.Username
                };

                var employee =
                    _employeeService.UpdateEmployee(
                        id,
                        updatedEmployee,

                        // Later this comes from JWT/claims.
                        CurrentEmployeeId
                    );

                return Ok(
                    MapToResponse(employee)
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


        // Employee Entity → EmployeeResponseDto
        private EmployeeResponseDto MapToResponse(
            Employee employee)
        {
            return new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Address = employee.Address,
                Contact = employee.Contact,
                Email = employee.Email,
                Role = employee.Role,
                StartDate = employee.StartDate,
                EndDate = employee.EndDate,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Username = employee.Username,
                CreatedAt = employee.CreatedAt,
                UpdatedAt = employee.UpdatedAt
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