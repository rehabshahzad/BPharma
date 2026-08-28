using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PharmacyMangementSystem.Security
{
    public class JwtAuthenticationHandler : DelegatingHandler //intercepts http before controller
    {
        protected override async Task<HttpResponseMessage> SendAsync( //SendAsync runs for every request
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authorization = request.Headers.Authorization; //Auth: Bearer(auth scheme)  eyshijsijdid....(jwt token)

            if (authorization != null &&
                authorization.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase)) //ignore difference of capitalization
            {
                string token = authorization.Parameter;//the actual token extraction

                try
                {
                    string secretKey =
                        ConfigurationManager.AppSettings["JwtSecretKey"];

                    var securityKey = new SymmetricSecurityKey( //converts the secret string into bytes then creates a cryptographic key from it
                        Encoding.UTF8.GetBytes(secretKey)
                    );

                    var validationParameters = new TokenValidationParameters //rules for checking JWT
                    {
                        ValidateIssuerSigningKey = true, //JWT signed by my secret key?
                        IssuerSigningKey = securityKey,

                        ValidateIssuer = false, //i don care who issued the key
                        ValidateAudience = false, //i don care who is it for

                        ValidateLifetime = true, //expiry??

                        ClockSkew = TimeSpan.Zero //no  grace period given
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();//NET class for working w jwt (read, create, validate JWT)

                    ClaimsPrincipal principal = tokenHandler.ValidateToken( //claims available thru principal
                        token,
                        validationParameters,
                        out SecurityToken validatedToken
                    );

                    Thread.CurrentPrincipal = principal; //This is the authenticated user. .NET KNOWS WHO DA USER IS

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal; //ASP.NET KNOWS WHO DA USER IS
                    }
                }
                catch
                {
                    // Invalid/expired token.
                    // We don't authenticate the request.
                    // [Authorize] will reject it.
                }
            }

            return await base.SendAsync(request, cancellationToken); //I've finished my security check. Now continue sending this request through the normal HTTP pipeline.
        }
    }
}