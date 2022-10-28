# Bookshop
## Easy way to run this project

I recommend using Visual Studio 2022 for this project, since it was created using this version.
Older versions might work fine though.

Steps:
1. This project uses Microsoft SQL Server as a database provider. 
If you are not sure how to set up a database just download 
[SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) and you will be fine.
Before running the project you should [create a database](https://support.mailessentials.gfi.com/hc/en-us/articles/360015116400-How-to-create-a-new-database-in-Microsoft-SQL-Server) named **Bookshop**
(if you intend to use default connection string defined in appsettings.json e.g.
```Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=Bookshop;```)

2. Next step would be restoring missing nuget packages. You can find some information about it [here](https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore).
3. If at this point the solution compiles, you will need to do one more thing. Run command ```Update-Database -Project Bookshop.DataLayer -StartUpProject Bookshop.WebApp```
in Package Manager Console (you can find it in VStudio menu by navigating to Tools > NuGet Package Manager > Package Manager Console).
This command will apply current migration file.
4. Install [Smtp4dev](https://github.com/rnwood/smtp4dev) for [email confirmation](https://github.com/jusrus01/Bookshop/commit/e2c6be2c39a3de2e5e901893a5427f09a5199453) to work (run it when creating new accounts).

After this project should run fine.

## Development notes
### ~~Application state in [initial commit](https://github.com/jusrus01/Bookshop/commit/7769906f8b0a9c017eab7898d26859d074111b91)~~
~~I implemented authentication/authorization with the use of ASP.NET Core Identity framework that relies on Entity Framework Core.
Implementation details are not important, so I will just discuss usage.~~

```c#
using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookshop.WebApp.Pages
{
    // Only users that are authenticated and have these roles can access this Razor Page model
    [RolesAuthorize(BookshopRoles.Administrator, BookshopRoles.Client)]
    public class PrivacyModel : PageModel
    {
        public PrivacyModel()
        {
        }
    }
}
```
Apparently, we can only authorize the whole page with the authorization attribute. If you need to check whether or not the user has another role (and global page authorization is not enough), you can use the **BookshopPageModel.UserHasRole()** function (also, all your newly created razor pages should inherit from **BookshopPageModel** instead of PagedModel). For example .cshtml:
```c#
@page
@using Bookshop.Contracts.Constants
@model Bookshop.WebApp.Pages.Client.ProfileModel
@{
}


@if (Model.UserHasRole(BookshopRoles.Administrator))
{
    <p>admin</p>
}

@if (Model.UserHasRole(BookshopRoles.Client))
{
    <p>client</p>
}
```
Or in .cs files:
```c#
public void OnGetAsync()
{
    if (!UserHasRole(BookshopRoles.Client))
    {
        // do something
    }

    // do something else
}
```

~~Also, Login/Register forms are not very well created. I guess we will need to decide on some kind of design or something :)~~

### Overview of project structure
This probably is not the best project structure, however my main aim was that it would be simple to use.

Projects:
- Bookshop.Contracts, contains interfaces for services and DTOs.
- Bookshop.DataLayer, contains entity models and DbContext.
- Bookshop.BusinessLogic, should contain service implementations.
- Bookshop.WebApp, contains Razor Pages, attributes and view models.

...

*Probably will discuss all of this IRL, since I am too lazy to type everything out :)*

### Some other helpful notes
- Using EF Core and applying database change (adding, editing, removing) you should always call DbContext.SaveChangesAsync() or DbContext.SaveChanges() afterwards, otherwise
there wont be any changes applied to database.
- When creating Razor Page it has to be in Pages folder. Also naming matters (so make sure that methods have appropriate names, otherwise nothing will work). I used these projects
as reference [1](https://github.com/simaosoares/WebAppIdentity/blob/master/Areas/Identity/Pages/Account/Register.cshtml.cs), [2](https://github.com/hinault/RazorDemo/blob/master/RazorDemo/Models/Student.cs)
- Added a [demo branch](https://github.com/jusrus01/Bookshop/commits/demo) where I tried to show how I think things in here should be created. View commits from bottom to top.

