using BankingAPI.Application.Interfaces;
using BankingAPI.Infrastructure.Context;
using BankingAPI.Infrastructure.Repositories;
using BankingAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BankingAPI.Infrastructure {
    public static class InterfaceServiceRegistration {
        public static IServiceCollection AddInterfaceServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<BankDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("BankWebAPIconnString"));


            });
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("BankWebAPIconnString"));


            });

            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}
