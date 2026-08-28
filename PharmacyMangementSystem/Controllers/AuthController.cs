using Google.Protobuf.WellKnownTypes;
using Pharma.BLL.Security;
using PharmacyMangementSystem.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers

{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Login data is required.");
            }

            string token = _authService.Login(
                dto.Username,
                dto.Password
            );

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Token = token
            });
        }
    }
}
