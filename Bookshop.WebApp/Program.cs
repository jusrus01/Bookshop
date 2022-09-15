using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

async Task InitializeDefinedRolesAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var definedRoles = typeof(BookshopRoles)
        .GetFields(BindingFlags.Public | BindingFlags.Static);

    foreach (var roleProperty in definedRoles)
    {
        if (!await roleManager.RoleExistsAsync(roleProperty.Name))
        {
            await roleManager.CreateAsync(new IdentityRole(roleProperty.Name));
        }
    }
}

void AddRequiredConfigurations(WebApplicationBuilder builder)
{
    var configuration = builder.Configuration;
    var services = builder.Services;

    services.AddDefaultDatabase(configuration);
    services.AddIndentity();
    services.AddServices();

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddRazorPages();
}

void UseRequiredConfigurations(WebApplication app)
{
    // Configure the HTTP request pipeline
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
    }

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();
}

var builder = WebApplication.CreateBuilder(args);

AddRequiredConfigurations(builder);

var app = builder.Build();

UseRequiredConfigurations(app);

await InitializeDefinedRolesAsync(app);

app.Run();