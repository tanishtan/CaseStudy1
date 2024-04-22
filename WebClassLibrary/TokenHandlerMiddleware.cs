using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClassLibrary
{
    public class TokenHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey;

        public TokenHandlerMiddleware(RequestDelegate next, string secretKey)
        {
            _next = next;
            _secretKey = secretKey;
        }

        public async Task Invoke(HttpContext context)
        {
            // Extract token from authorization header (replace 'Authorization' with your header name if different)
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').LastOrDefault();

            if (token != null)
            {
                try
                {
                    var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey)),
                        ValidateIssuer = false, // Adjust based on your issuer validation needs
                        ValidateAudience = false, // Adjust based on your audience validation needs
                        ValidateLifetime = true, // Set to true for validating token expiry
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                    context.Items["User"] = principal; // Attach user information to context (adjust based on your needs)
                }
                catch (SecurityTokenException ex)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Handle unauthorized access
                    await context.Response.WriteAsync(ex.Message);
                    return;
                }
            }

            await _next(context); // Pass control to the next middleware or controller
        }
    }
}
