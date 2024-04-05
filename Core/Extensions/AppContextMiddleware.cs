using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using ServiceStack.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class AppContextMiddleware
    {
        private readonly RequestDelegate _next;

        public AppContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken != null)
                {
                    var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (!int.TryParse(userIdClaim, out int userId))
                    {
                        throw new Exception("UserId claim is missing or not a valid integer.");
                    }

                    var organizationIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "OrganizationId")?.Value;
                    int organizationId;
                    if (!int.TryParse(organizationIdClaim, out organizationId))
                    {
                        throw new Exception("OrganizationId claim is missing or not a valid integer.");
                    }

                    var appContext = new AppContext
                    {
                        UserId = userId,
                        OrganizationId = organizationId
                    };

                    context.Items["AppContext"] = appContext;
                }
            }

            await _next(context);
        }
    }
}