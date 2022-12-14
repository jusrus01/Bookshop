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

        private readonly IUnitOfWork _uow;
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
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task UpdateAsync(EditClientDto editDto)
        {
            var client = await _userManager.FindByIdAsync(editDto.Id);
            MapEditDtoToUser(editDto, client);
            await _userManager.UpdateAsync(client);
            await ChangeRoleIfNeededAsync(editDto, client);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync() =>
            _mapper.Map<IEnumerable<RoleDto>>(await _uow.GetDbSet<ApplicationRole>().ToListAsync());

        public async Task<Paged<PartialClientDto>> GetPagedAsync(int page, int pageSize)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();
            return await _usersDbSet.OrderByDescending(user => user.Id == authenticatedUserId)
                .ThenBy(user => user.FirstName)
                .ThenBy(user => user.LastName)
                .ToPagedAsync(MapUserToPartialClient(), page, pageSize);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            EnsureValidUser(user);
            
            try
            {
                await _userManager.DeleteAsync(user);
            }
            catch
            {
                throw new Exception("This user has associated data. Cannot delete.");
            }
        }

        public async Task<ClientDto> GetAsync(string clientId)
        {
            var user = await _usersDbSet.SingleAsync(user => user.Id == clientId);
            var roles = await _userManager.GetRolesAsync(user);
            return _mapper.Map<ApplicationUser, ClientDto>(user,
                opt => opt.AfterMap((_, client) => client.Roles = roles));
        }

        private static void EnsureValidUser(ApplicationUser user)
        {
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
        }

        private async Task ChangeRoleIfNeededAsync(EditClientDto editDto, ApplicationUser client)
        {
            if (editDto.Role != editDto.SelectedRole)
            {
                await _userManager.RemoveFromRoleAsync(client, editDto.Role);
                await _userManager.AddToRoleAsync(client, editDto.SelectedRole);
            }
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

        private static void MapEditDtoToUser(EditClientDto editDto, ApplicationUser client)
        {
            client.PhoneNumber = editDto.PhoneNumber;
            client.FirstName = editDto.FirstName;
            client.LastName = editDto.LastName;
            client.Address = editDto.Address;
            client.Email = editDto.Email;
        }
    }
}
