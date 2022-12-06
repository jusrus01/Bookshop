using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IClientService
    {
        Task DeleteAsync(string id);

        Task<ClientDto> GetAsync(string id);

        Task<Paged<PartialClientDto>> GetPagedAsync(int page, int pageSize);
    }
}
