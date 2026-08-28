
using Microsoft.IdentityModel.Tokens;
using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;

namespace Pharma.BLL.Security
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(Employee employee)
        {
            string secretKey = ConfigurationManager.AppSettings["JwtSecretKey"];

            var securityKey = new SymmetricSecurityKey( //symm: same secret key 
                Encoding.UTF8.GetBytes(secretKey)  //cryptographic algos work w bytes so we are convertingthe secret key into it
            );                                    //Here are the bytes of my secret. Treat them as the key used for JWT signing.

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256  //security key+ signing algo
            );

            var claims = new List<Claim>
            {
                new Claim("EmployeeId", employee.EmployeeId.ToString()), //claims are just info about the employee
                new Claim(ClaimTypes.Name, employee.Username),
                new Claim(ClaimTypes.Role, employee.Role.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), //expire after 2 hours
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token); //converts the token object in tro string for frontend
        }
    }
}