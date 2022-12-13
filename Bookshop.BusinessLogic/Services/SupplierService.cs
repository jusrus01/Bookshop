using AutoMapper;
using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Supplier = Bookshop.DataLayer.Models.Supplier;

namespace Bookshop.BusinessLogic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly DbSet<Supplier> _supplierDbSet;
        private readonly DbSet<City> _citiesDbSet;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SupplierService(
            IUnitOfWork uow,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _uow = uow;
            _supplierDbSet = uow.GetDbSet<Supplier>();
            _citiesDbSet = uow.GetDbSet<City>();
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Paged<SupplierDto>> GetSuppliersPagedAsync(int page, int pageSize)
        {
            return await _supplierDbSet.OrderByDescending(supplier => supplier.Id)
                .ToPagedAsync(supplier => new SupplierDto
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    PhoneNumber = supplier.PhoneNumber,
                    Address = supplier.Address,
                    CityId = supplier.CityId
                },
                page,
                pageSize);
        }

        public async Task<List<SupplierDto>> GetSuppliersList()
        {
            List<Supplier> dbSuppliers = _supplierDbSet.ToList();

            List<SupplierDto> suppliers = new List<SupplierDto>();

            for (int i = 0; i < dbSuppliers.Count; i++)
            {
                suppliers.Add(new SupplierDto
                {
                    Id = dbSuppliers[i].Id,
                    Name = dbSuppliers[i].Name,
                    Email = dbSuppliers[i].Email,
                    PhoneNumber = dbSuppliers[i].PhoneNumber,
                    Address = dbSuppliers[i].Address,
                    CityId = dbSuppliers[i].CityId
                });
            }

            return suppliers;
        }

        public async Task<List<CityDto>> GetCitiesList()
        {
            List<City> dbCities = _citiesDbSet.ToList();

            List<CityDto> cities = new List<CityDto>();

            for (int i = 0; i < dbCities.Count; i++)
            {
                cities.Add(new CityDto
                {
                    Id = dbCities[i].Id,
                    Name = dbCities[i].Name,
                    Code = dbCities[i].Code
                });
            }

            return cities;
        }

        public async Task AddAsync(SupplierDto supplierDto)
        {
            await CreateNewSupplier(supplierDto);
        }

        public async Task<SupplierDto> CreateNewSupplier(SupplierDto supplierDto)
        {
            var newSupplier = new SupplierDto
            {
                Name = supplierDto.Name,
                Email = supplierDto.Email,
                PhoneNumber = supplierDto.PhoneNumber,
                Address = supplierDto.Address,
                CityId = supplierDto.CityId
            };
            _supplierDbSet.Add(_mapper.Map<Supplier>(newSupplier));
            await _uow.SaveChangesAsync();
            return newSupplier;
        }

    }
}
