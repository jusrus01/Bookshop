using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.BusinessLogic.Services
{
    public class ClientService : IClientService
    {
        private readonly DbSet<ApplicationUser> _usersDbSet;

        public ClientService(IUnitOfWork uow)
        {
            _usersDbSet = uow.GetDbSet<ApplicationUser>();
        }

        public async Task<ClientDto> GetClientAsync(string clientId)
        {
            var user = await _usersDbSet.SingleOrDefaultAsync(user => user.Id == clientId);

            if (user == null)
            {
                throw new Exception("Client not found");
            }

            return new ClientDto
            {
                Email = user.Email
            };
        }
    }
}
