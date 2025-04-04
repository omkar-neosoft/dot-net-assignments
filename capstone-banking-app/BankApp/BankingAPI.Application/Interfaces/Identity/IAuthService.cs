using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Application.Models.Identity;
using Microsoft.AspNetCore.Http;

namespace BankingAPI.Application.Interfaces.Identity {
    public interface IAuthService {
        Task<AuthResponse> Login(AuthRequest authRequest, HttpContext httpContext);
        Task<AuthResponse> Register(RegistrationRequest registrationRequest, HttpContext httpContext);
    }
}
