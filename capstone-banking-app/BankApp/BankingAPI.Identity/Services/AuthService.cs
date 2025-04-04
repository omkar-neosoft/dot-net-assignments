using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Application.Exceptions;
using BankingAPI.Application.Interfaces.Identity;
using BankingAPI.Application.Models.Identity;
using BankingAPI.Identity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BankingAPI.Identity.Services {
    public class AuthService : IAuthService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings) {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest authRequest, HttpContext httpContext) {
            var user = await _userManager.FindByEmailAsync(authRequest.Email);
            if (user == null) {
                throw new NotFoundException($"User with Email {authRequest.Email} does not exist.");
            }

            var userPassword = await _signInManager.CheckPasswordSignInAsync(user, authRequest.Password, false);
            if (!userPassword.Succeeded) {
                throw new BadRequestException("Invalid password credentials.");
            }

            // Generate JWT Token
            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var response = new AuthResponse {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = tokenString
            };

            // ✅ Store token in HTTP-only cookie
            var cookieOptions = new CookieOptions {
                HttpOnly = true,  // Prevents XSS attacks
                Secure = true,    // Ensures cookie is sent only over HTTPS
                SameSite = SameSiteMode.Strict, // Prevents CSRF attacks
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };

            httpContext.Response.Cookies.Append("AuthToken", tokenString, cookieOptions);  // 🔥 Store token in cookies

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user) {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }

        public async Task<AuthResponse> Register(RegistrationRequest registrationRequest, HttpContext httpContext) {
            var user = new ApplicationUser {
                Email = registrationRequest.Email,
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                UserName = registrationRequest.Username,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, "User");
                //return new RegistrationResponse() { UserId = user.Id };

                // Generate JWT Token
                JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                var response = new AuthResponse {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = tokenString
                };

                // ✅ Store token in HTTP-only cookie
                var cookieOptions = new CookieOptions {
                    HttpOnly = true,  // Prevents XSS attacks
                    Secure = true,    // Ensures cookie is sent only over HTTPS
                    SameSite = SameSiteMode.Strict, // Prevents CSRF attacks
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
                };

                httpContext.Response.Cookies.Append("AuthToken", tokenString, cookieOptions);

                return response;

            } else {
                var errorResult = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"{errorResult}");
            }
        }
    }
}
