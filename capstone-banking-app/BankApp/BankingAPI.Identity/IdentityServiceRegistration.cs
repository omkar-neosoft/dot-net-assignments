using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Application.Interfaces.Identity;
using BankingAPI.Application.Models.Identity;
using BankingAPI.Identity.Context;
using BankingAPI.Identity.Model;
using BankingAPI.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BankingAPI.Identity {
    public static class IdentityServiceRegistration {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration) {
            //services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddDbContext<BankingAPIIdentityDbContext>(options =>

                options.UseSqlServer(configuration.GetConnectionString("BankWebAPIconnString")));

            services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<BankingAPIIdentityDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>();
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {

                //o.Events = new JwtBearerEvents {
                //    OnMessageReceived = context => {
                //        // ✅ Read token from cookies instead of Authorization header
                //        var accessToken = context.Request.Cookies["AuthToken"]; // Cookie name should match Angular
                //        if (!string.IsNullOrEmpty(accessToken)) {
                //            context.Token = accessToken;
                //        }
                //        return Task.CompletedTask;
                //    }
                //};

                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });



            return services;
        }
    }
}
