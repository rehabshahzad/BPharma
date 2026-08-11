using Pharma.DAL.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pharma.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PharmacyDbContext _context; //This stores the database context that the repository will use.

        public EmployeeRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees
                .FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.AsNoTracking().ToList(); //i added as no tracking for better memory usage
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
        }

       

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool EmployeeUsernameExists(
    string username,
    int? excludeEmployeeId = null)
        {
            return _context.Employees.Any(e => //by using Any() after the first matching record it gives you value so its better than counting all records (SAVES MEMORY)
                e.Username == username &&
                (!excludeEmployeeId.HasValue ||
                 e.EmployeeId != excludeEmployeeId.Value));
        }

        public bool EmployeeEmailExists(
            string email,
            int? excludeEmployeeId = null)
        {
            return _context.Employees.Any(e =>
                e.Email == email &&
                (!excludeEmployeeId.HasValue ||
                 e.EmployeeId != excludeEmployeeId.Value));
        }
    }
}