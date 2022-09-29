using Bookshop.BusinessLogic.Services;
using Bookshop.Contracts;
using Bookshop.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Bookshop.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.WebApp.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddIndentity(this IServiceCollection services)
        {
            // Development settings
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Activate email confirmation
                // options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<BookshopDbContext>()
            .AddDefaultTokenProviders();
        } 

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
        }

        public static void AddDefaultDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookshopDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Default")));
        }
    }
}
