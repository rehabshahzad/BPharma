using PharmacyManagement.Entity.Entities;
using System.Collections.Generic;

public interface IEmployeeRepository
{
    Employee GetEmployeeById(int id);

    List<Employee> GetAllEmployees();

    void AddEmployee(Employee employee);

    bool EmployeeUsernameExists(
        string username,
        int? excludeEmployeeId = null
    );

    bool EmployeeEmailExists(
        string email,
        int? ExcludeEmployeeId = null
    );

    void SaveChanges();
    Employee GetEmployeeByUsername( string username );
}