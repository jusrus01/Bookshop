using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts.Generics;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Linq.Expressions;

namespace Bookshop.BusinessLogic.Services
{
    public class ClientService : IClientService
    {
        private readonly DbSet<ApplicationUser> _usersDbSet;
        private readonly UserManager<ApplicationUser> _userManager;
        
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService(
            IUnitOfWork uow,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _usersDbSet = uow.GetDbSet<ApplicationUser>();
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Paged<PartialClientDto>> GetClientsPagedAsync(int page, int pageSize)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();
            return await _usersDbSet.OrderByDescending(user => user.Id == authenticatedUserId)
                .ThenBy(user => user.FirstName)
                .ThenBy(user => user.LastName)
                .ToPagedAsync(MapUserToPartialClient(), page, pageSize);
        }

        public async Task<ClientDto> GetClientAsync(string clientId)
        {
            var user = await _usersDbSet.SingleAsync(user => user.Id == clientId);
            var roles = await _userManager.GetRolesAsync(user);
            return _mapper.Map<ApplicationUser, ClientDto>(user,
                opt => opt.AfterMap((_, client) => client.Roles = roles));
        }

        private static Expression<Func<ApplicationUser, PartialClientDto>> MapUserToPartialClient() =>
            user => new PartialClientDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Email = user.Email,
                LastLogin = user.LastLogin
            };
    }
}
