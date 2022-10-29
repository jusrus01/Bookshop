using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IClientService
    {
        Task<ClientDto> GetClientAsync(string clientId);

        Task<Paged<PartialClientDto>> GetClientsPagedAsync(int page, int pageSize);
    }
}
