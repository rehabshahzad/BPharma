using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyManagement.Entity.Entities;

namespace Pharma.BLL.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();

        Employee GetEmployeeById(int employeeId);

        Employee CreateEmployee(
          
            Employee employee,
            string temporaryPassword, //to hash it and save
            int createdByEmployeeId
        );

        Employee UpdateEmployee(
            int employeeId,
            Employee updatedEmployee,
            int updatedByEmployeeId
        );

       

    }
}
