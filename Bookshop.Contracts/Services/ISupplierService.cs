using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface ISupplierService
    {
        Task<Paged<SupplierDto>> GetSuppliersPagedAsync(int page, int pageSize);
        public Task<List<CityDto>> GetCitiesList();
        public Task<List<SupplierDto>> GetSuppliersList();

        public Task AddAsync(SupplierDto supplierDto);

        public Task<SupplierDto> CreateNewSupplier(SupplierDto supplierDto);


    }
}
