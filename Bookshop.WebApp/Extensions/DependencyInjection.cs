using Bookshop.BusinessLogic.Services;
using Bookshop.Contracts;
using Bookshop.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Bookshop.DataLayer;
using Microsoft.EntityFrameworkCore;
using Bookshop.DataLayer.Models;

namespace Bookshop.WebApp.Extensions
{
    public static class DependencyInjection
    {
        public static void AddIndentity(this IServiceCollection services)
        {
            // Development settings
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Activate email confirmation
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<BookshopDbContext>()
            .AddDefaultTokenProviders();
        } 

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IClientService, ClientService>();
        }

        public static void AddDefaultDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookshopDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Default")));
        }
    }
}
