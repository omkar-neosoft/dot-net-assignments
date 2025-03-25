using EventManagementSystem.Data;
using EventManagementSystem.Filters;
using EventManagementSystem.Models;
using EventManagementSystem.Repositories;
using EventManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add Identity with Roles
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //builder.Services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddMemoryCache();

            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.Configure<IdentityOptions>(options => {
                options.SignIn.RequireConfirmedEmail = false;
            });

            builder.Services.AddControllersWithViews(options => {
                options.Filters.Add<GlobalExceptionFilter>(); // Add global exception filter
            });

            builder.Services.ConfigureApplicationCookie(options => {
                options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect unauthorized users to this page
            });

            // Ading Repository and services
            builder.Services.AddScoped<ITicketBookingRepository, TicketBookingRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();

            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ITicketBookingService, TicketBookingService>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            var app = builder.Build();

            // Create Roles and Default Admin User
            using (var scope = app.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                await SeedRolesAndAdmin(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }

        private static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Organizer", "User" };

            foreach (var roleName in roleNames) {
                if (!await roleManager.RoleExistsAsync(roleName)) {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 🔹 Create Default Admin User
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null) {
                var newAdmin = new ApplicationUser {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }

            // 🔹 Create Default organizer User
            string organizerEmail = "organizer@example.com";
            string organizerPassword = "Organizer@123";

            var organizerUser = await userManager.FindByEmailAsync(organizerEmail);
            if (organizerUser == null) {
                var newOrganizer = new ApplicationUser {
                    UserName = organizerEmail,
                    Email = organizerEmail,
                    FirstName = "Organizer",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newOrganizer, organizerPassword);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(newOrganizer, "Organizer");
                }
            }

            // 🔹 Create Default user User
            string userEmail = "user@example.com";
            string userPassword = "User@123";

            var userUser = await userManager.FindByEmailAsync(userEmail);
            if (userUser == null) {
                var newuser = new ApplicationUser {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = "User",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newuser, userPassword);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(newuser, "User");
                }
            }
        }
    }
}



# region latest changes
//using EventManagementSystem.Data;
//using EventManagementSystem.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace EventManagementSystem {
//    public class Program {
//        public static void Main(string[] args) {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(connectionString));
//            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//            //    .AddEntityFrameworkStores<ApplicationDbContext>();
//            //builder.Services.AddControllersWithViews();

//            // Register Identity with ApplicationUser
//            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                .AddEntityFrameworkStores<ApplicationDbContext>();
//            builder.Services.AddControllersWithViews();

//            builder.Services.AddMemoryCache();
//            builder.Services.AddSession(options => {
//                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your timeout here
//            });

//            builder.Services.Configure<IdentityOptions>(options => {
//                options.SignIn.RequireConfirmedEmail = false; // Allow login without email confirmation
//            });

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment()) {
//                app.UseMigrationsEndPoint();
//            } else {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthorization();

//            app.UseSession();

//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");
//            app.MapRazorPages();

//            app.Run();
//        }
//    }
//}

#endregion 

//namespace EventManagementSystem {
//    public class Program {
//        public static void Main(string[] args) {
//            var builder = WebApplication.CreateBuilder(args);

//            // ✅ Database Configuration
//            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(connectionString));

//            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//            // ✅ Register Identity with ApplicationUser
//            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddDefaultTokenProviders();

//            // ✅ Fix: Add Razor Pages (Required for Identity)
//            builder.Services.AddControllersWithViews();
//            builder.Services.AddRazorPages();

//            var app = builder.Build();

//            if (app.Environment.IsDevelopment()) {
//                app.UseMigrationsEndPoint();
//            } else {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();
//            app.UseRouting();

//            // ✅ Enable Authentication & Authorization
//            app.UseAuthentication();
//            app.UseAuthorization();

//            // ✅ Route Configuration
//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");

//            // ✅ Ensure Identity Razor Pages are mapped
//            app.MapRazorPages();

//            app.Run();
//        }
//    }
//}
