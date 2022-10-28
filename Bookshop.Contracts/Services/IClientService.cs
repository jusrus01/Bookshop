using Bookshop.Contracts.DataTransferObjects.Clients;

namespace Bookshop.Contracts.Services
{
    public interface IClientService
    {
        Task<ClientDto> GetClientAsync(string clientId);
    }
}
