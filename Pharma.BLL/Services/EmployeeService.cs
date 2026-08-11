using Pharma.BLL.Security;
using Pharma.DAL.Repositories;
using Pharma.Entity.Enums;
using PharmacyManagement.Entity.Entities;
using System; //for DateTime and Exceptions
using System.Collections.Generic; //List<Employee> type stuff
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.BLL.Services
{//layer for business logic
    //checks input and applies business rules
    //calls repo for db operations
    public class EmployeeService : IEmployeeService

    {
        private readonly IEmployeeRepository _employeeRepository;
        //readonly: after this var gets value from constructor, it cant be replaced by another repo object

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository; //Dependency Injection = Passing an object a class needs from outside instead of creating it inside the class.
        }

        //GET BY ID
        public Employee GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Employee Id must be greater than 0");//method is given an invalid argument
            }
            return _employeeRepository.GetEmployeeById(id);
        }
        //GET ALL
        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        //CREATE
        public Employee CreateEmployee(
    Employee employee,
    string temporaryPassword,
    int createdByEmployeeId
    )
        {
            ValidateEmployee(employee);

            if (string.IsNullOrWhiteSpace(temporaryPassword))
            {
                throw new ArgumentException(
                    "Temporary password is required."
                );
            }

            if (temporaryPassword.Length < 8)
            {
                throw new ArgumentException(
                    "Temporary password must be at least 8 characters."
                );
            }

            if (_employeeRepository.EmployeeUsernameExists(employee.Username))
            {
                throw new InvalidOperationException(
                    "Username already exists."
                );
            }

            if (_employeeRepository.EmployeeEmailExists(employee.Email))
            {
                throw new InvalidOperationException(
                    "Email already exists."
                );
            }

            if (createdByEmployeeId <= 0)
            {
                throw new ArgumentException(
                    "Created-by employee ID is required."
                );
            }

            var creator =
                _employeeRepository.GetEmployeeById(
                    createdByEmployeeId
                );

            if (creator == null)
            {
                throw new ArgumentException(
                    "Creating employee does not exist."
                );
            }

            employee.CreatedByEmployeeId = createdByEmployeeId;
            employee.FirstName = employee.FirstName.Trim();
            employee.LastName = employee.LastName.Trim();
            employee.Address = employee.Address.Trim();
            employee.Contact = employee.Contact.Trim();
            employee.Email = employee.Email.Trim();
            employee.Username = employee.Username.Trim();

            employee.TempPasswordHash =
                PasswordHasher.HashPassword(temporaryPassword);

            employee.IsActive = true;
         

            // Employee has not replaced temporary password yet.
            employee.IsPasswordChanged = false;

            employee.EndDate = null;

            employee.CreatedAt = DateTime.Now;

            employee.UpdatedByEmployeeId = null;
            employee.UpdatedAt = null;

            _employeeRepository.AddEmployee(employee);
            _employeeRepository.SaveChanges();

            return employee;
        }

        //UPDATE
        public Employee UpdateEmployee(
    int employeeId,
    Employee updatedEmployee,
    int updatedByEmployeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");

            if (updatedByEmployeeId <= 0)
                throw new ArgumentException(
                    "Updated-by employee ID is required."
                );

            ValidateEmployee(updatedEmployee);

            var existingEmployee =
                _employeeRepository.GetEmployeeById(employeeId);

            if (existingEmployee == null)
                throw new KeyNotFoundException(
                    "Employee was not found."
                );

            var updater =
                _employeeRepository.GetEmployeeById(updatedByEmployeeId);

            if (updater == null)
                throw new ArgumentException(
                    "Updating employee does not exist."
                );

            if (_employeeRepository.EmployeeUsernameExists(
                    updatedEmployee.Username,
                    employeeId))
            {
                throw new InvalidOperationException(
                    "Username already exists."
                );
            }

            if (_employeeRepository.EmployeeEmailExists(
                    updatedEmployee.Email,
                    employeeId))
            {
                throw new InvalidOperationException(
                    "Email already exists."
                );
            }

            if (!updatedEmployee.IsActive &&
                !updatedEmployee.EndDate.HasValue)
            {
                throw new ArgumentException(
                    "End date is required for an inactive employee."
                );
            }

            if (updatedEmployee.IsActive &&
                updatedEmployee.EndDate.HasValue)
            {
                throw new ArgumentException(
                    "Active employee cannot have an end date."
                );
            }

            existingEmployee.FirstName =
                updatedEmployee.FirstName.Trim();

            existingEmployee.LastName =
                updatedEmployee.LastName.Trim();

            existingEmployee.Address =
                updatedEmployee.Address.Trim();

            existingEmployee.Contact =
                updatedEmployee.Contact.Trim();

            existingEmployee.Email =
                updatedEmployee.Email.Trim();

            existingEmployee.Role =
                updatedEmployee.Role;

            existingEmployee.StartDate =
                updatedEmployee.StartDate;

            existingEmployee.EndDate =
                updatedEmployee.EndDate;

            existingEmployee.Salary =
                updatedEmployee.Salary;

            existingEmployee.IsActive =
                updatedEmployee.IsActive;

            existingEmployee.Username =
                updatedEmployee.Username.Trim();

            existingEmployee.UpdatedByEmployeeId =
                updatedByEmployeeId;

            existingEmployee.UpdatedAt =
                DateTime.Now;

            _employeeRepository.SaveChanges();

            return existingEmployee;
        }

       
        //REUSABLE FOR CREATE AND UPDATE
        private void ValidateEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (string.IsNullOrWhiteSpace(employee.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(employee.LastName))
                throw new ArgumentException("Last name is required.");

            if (string.IsNullOrWhiteSpace(employee.Address))
                throw new ArgumentException("Address is required.");

            if (string.IsNullOrWhiteSpace(employee.Contact))
                throw new ArgumentException("Contact is required.");

            if (string.IsNullOrWhiteSpace(employee.Email))
                throw new ArgumentException("Email is required.");

            if (!IsValidEmail(employee.Email))
                throw new ArgumentException("Email format is invalid.");

            if (string.IsNullOrWhiteSpace(employee.Username))
                throw new ArgumentException("Username is required.");

            if (!Enum.IsDefined(typeof(EmployeeRole), employee.Role))
                throw new ArgumentException("Employee role is invalid.");

            if (employee.StartDate == default(DateTime))
                throw new ArgumentException("Start date is required.");

            if (employee.EndDate.HasValue &&
                employee.EndDate.Value.Date < employee.StartDate.Date)
            {
                throw new ArgumentException(
                    "End date cannot be earlier than start date."
                );
            }

            if (employee.Salary < 0)
                throw new ArgumentException("Salary cannot be negative.");
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email.Trim());

                return address.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}