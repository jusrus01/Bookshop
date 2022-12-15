using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IClientService
    {
        Task DeleteAsync(string id);

        Task<ClientDto> GetAsync(string id);
        
        Task<IEnumerable<ClientReportOrderDto>> GetOrderHistoryAsync(string id);

        Task<byte[]> GetOrderHistoryPdfAsync(string userId);

        Task<Paged<PartialClientDto>> GetPagedAsync(int page, int pageSize);

        Task<IEnumerable<RoleDto>> GetRolesAsync();
            
        Task UpdateAsync(EditClientDto client);
    }
}
