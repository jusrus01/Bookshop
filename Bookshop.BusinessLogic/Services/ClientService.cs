using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts.Generics;
using Microsoft.AspNetCore.Identity;

namespace Bookshop.BusinessLogic.Services
{
    public class ClientService : IClientService
    {
        private readonly DbSet<ApplicationUser> _usersDbSet;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService(
            IUnitOfWork uow,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _usersDbSet = uow.GetDbSet<ApplicationUser>();

            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Paged<PartialClientDto>> GetClientsPagedAsync(int page, int pageSize)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();

            return await _usersDbSet.OrderByDescending(user => user.Id == currentUserId)
                .ThenBy(user => user.FirstName)
                .ThenBy(user => user.LastName)
                .ToPagedAsync(user => new PartialClientDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    LastLogin = user.LastLogin
                },
                page,
                pageSize);
        }

        public async Task<ClientDto> GetClientAsync(string clientId)
        {
            var user = await _usersDbSet.SingleOrDefaultAsync(user => user.Id == clientId);

            if (user == null)
            {
                throw new Exception("Client not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new ClientDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Created = user.Created,
                LastLogin = user.LastLogin,
                Roles = roles
            };
        }
    }
}
