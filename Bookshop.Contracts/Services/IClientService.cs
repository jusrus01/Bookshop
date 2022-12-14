using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IClientService
    {
        Task DeleteAsync(string id);

        Task<ClientDto> GetAsync(string id);
        
        Task<IEnumerable<ClientReportOrderDto>> GetOrdersAsync(string id);

        Task<Paged<PartialClientDto>> GetPagedAsync(int page, int pageSize);

        Task<IEnumerable<RoleDto>> GetRolesAsync();
            
        Task UpdateAsync(EditClientDto client);
    }
}
