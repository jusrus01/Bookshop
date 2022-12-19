using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Generics;
using SupplierDto = Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto;

namespace Bookshop.Contracts.Services
{
    public interface ISupplierService
    {
        Task<Paged<SupplierDto>> GetSuppliersPagedAsync(int page, int pageSize);
        public Task<List<CityDto>> GetCitiesList();
        public Task<List<SupplierDto>> GetSuppliersList();

        public Task AddAsync(SupplierDto supplierDto, List<int> storagesIdx);

        public Task CreateNewSupplier(SupplierDto supplierDto, List<int> storagesIdx);
        public Task DeleteSupplier(int? id);

        public Task<SupplierDto> GetSupplierById(int supplierID);
        public Task UpdateSupplierAsync(SupplierDto supplierDto, List<int> storagesIdx);

        public List<GenreDto> GetAllGenres();
        public List<SupplierBookDto> GetAllBooks();
        public List<StorageDto> GetAllStorages();
        public Task<List<StorageDto>> GetStoragesBySupplierId(int id);


    }
}
