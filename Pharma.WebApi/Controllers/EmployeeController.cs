using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.DAL.Repositories;
using PharmacyManagement.Entity.Entities;
using PharmacyMangementSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [Authorize( Roles = "Admin")]
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
       
        private readonly IEmployeeService _employeeService;
    

        public EmployeesController(IEmployeeService service)
        {
           _employeeService = service;
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
                        GetCurrentEmployeeId()
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
                        GetCurrentEmployeeId()
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