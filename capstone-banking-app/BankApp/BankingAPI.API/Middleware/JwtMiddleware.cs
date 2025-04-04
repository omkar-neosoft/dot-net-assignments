using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtMiddleware {
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public JwtMiddleware(RequestDelegate next, IConfiguration config) {
        _next = next;
        _config = config;
    }

    public async Task Invoke(HttpContext context) {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token)) {
            token = context.Request.Cookies["AuthToken"];
        }

        if (!string.IsNullOrEmpty(token)) {
            AttachUserToContext(context, token);
        }

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token) {
        try {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _config["JwtSettings:Issuer"],
                ValidAudience = _config["JwtSettings:Audience"],
                ValidateLifetime = true
            };

            var principal = tokenHandler.ValidateToken(token, parameters, out _);
            context.Items["UserId"] = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            context.Items["UserRole"] = principal.FindFirst(ClaimTypes.Role)?.Value;
        } catch {
            // Token is invalid
        }
    }
}
