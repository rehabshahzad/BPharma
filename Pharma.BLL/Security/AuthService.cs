using Pharma.BLL.Security;
using Pharma.DAL.Repositories;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;

namespace Pharma.BLL.Security
{
    public class AuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public AuthService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public string Login(string username, string password)

        {
            
            var employee = _employeeRepository.GetEmployeeByUsername(username);

            if (employee == null)
            {
                return null;
            }

            if (!employee.IsActive)
            {
                return null;
            }

            bool isPasswordCorrect = PasswordHasher.VerifyPassword(
                password,
                employee.PasswordHash
            );

            if (!isPasswordCorrect)
            {
                return null;
            }

            string token = JwtTokenGenerator.GenerateToken(employee);//creds are okay now create a jwt token for this employee
            return token;
        }

    }
}
