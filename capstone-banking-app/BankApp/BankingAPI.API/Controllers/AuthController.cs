using System.Net.Http;
using BankingAPI.Application.Interfaces.Identity;
using BankingAPI.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        readonly IAuthService _authService;
        public AuthController(IAuthService authService) {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest authRequest) {
            var response = await _authService.Login(authRequest, HttpContext);
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegistrationRequest request) {
            var response = await _authService.Register(request, HttpContext);
            return Ok(response);
        }
    }
}
