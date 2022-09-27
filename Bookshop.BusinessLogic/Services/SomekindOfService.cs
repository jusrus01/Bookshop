using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Demo;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models; // Add project reference here... I forgot
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.BusinessLogic.Services
{
    // We will add something to the database :)
    public class SomekindOfService : ISomekindOfService
    {
        private readonly IUnitOfWork _uow;

        private readonly DbSet<Demo> _demoDbSet;
        private readonly DbSet<IdentityUser> _userDbSet; // I recommend using UserManager instead if possible...

        // This is how dependency injection is used (MUST CONFIGURE IT IN ConfigurationExtensions.cs!!!)
        public SomekindOfService(IUnitOfWork uow, UserManager<IdentityUser> userManager) // Also, inject INTERFACES not their implementations
        {
            // This will magically give us UnitOfWork object :)
            // And with it we will extract Demo table like so:
            _demoDbSet = uow.GetDbSet<Demo>();
            _userDbSet = uow.GetDbSet<IdentityUser>();

            _uow = uow;
        }

        // Simple function to add something
        public async Task CreateStuffAsync(CreateDemoDto createDemoDto)
        {
            var anyExistingUser = await _userDbSet.FirstOrDefaultAsync(user => user.UserName != null); // Random condition :D
            
            var demo = new Demo
            {
                Name = createDemoDto.Name,
                Value = createDemoDto.Value,
                UserId = anyExistingUser.Id // Note: UserId should probably come from the current request. There is a way to get it.
            };

            // Add to database
            await _demoDbSet.AddAsync(demo);

            // CALL THIS ALWAYS AFTER DATABASE MODIFICATION THIS WILL UPDATE THE DATABASE
            // otherwise, nothing will be added
            await _uow.SaveChangesAsync();
        }
    }
}
