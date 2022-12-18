using AutoMapper;
using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Supplier = Bookshop.DataLayer.Models.Supplier;
using SupplierDto = Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto;

namespace Bookshop.BusinessLogic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly DbSet<Supplier> _supplierDbSet;
        private readonly DbSet<City> _citiesDbSet;
        private readonly DbSet<Genre> _genresDbSet;
        private readonly DbSet<Book> _booksDbSet;
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
            _genresDbSet = uow.GetDbSet<Genre>();
            _booksDbSet = uow.GetDbSet<Book>();
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

        public async Task CreateNewSupplier(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _supplierDbSet.Add(supplier);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteSupplier(int? id)
        {
            var supplier = await _supplierDbSet.SingleOrDefaultAsync(b => b.Id == id);

            if (supplier == null)
            {
                throw new Exception("No supplier found");
            }

            _supplierDbSet.Remove(supplier);
            await _uow.SaveChangesAsync();
        }

        public async Task<SupplierDto> GetSupplierById(int supplierID)
        {
            var supplier = await _supplierDbSet.FindAsync(supplierID);


            if (supplier == null)
            {
                throw new Exception("Supplier not found");
            }

            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task UpdateSupplierAsync(SupplierDto supplierDto)
        {
            //var remove = await _supplierDbSet.FindAsync(supplierDto.Id);
            //_supplierDbSet.Remove(remove);
            //await _uow.SaveChangesAsync();
            //supplierDto.Id = 0;
            //_supplierDbSet.Add(_mapper.Map<Supplier>(supplierDto));
            _supplierDbSet.Update(_mapper.Map<Supplier>(supplierDto));
            await _uow.SaveChangesAsync();
        }

        public List<GenreDto> GetAllGenres()
        {
            List<Genre> dbGenres =  _genresDbSet.ToList();

            List<GenreDto> genres = new List<GenreDto>();

            for (int i = 0; i < dbGenres.Count; i++)
            {
                genres.Add(new GenreDto
                {
                    Id = dbGenres[i].Id,
                    Name = dbGenres[i].Name
                });
            }

            return genres;
        }

        public List<SupplierBookDto> GetAllBooks()
        {
            List<Book> dbBooks = _booksDbSet.ToList();

            List<SupplierBookDto> books = new List<SupplierBookDto>();

            for (int i = 0; i < dbBooks.Count; i++)
            {
                books.Add(new SupplierBookDto
                {
                    ISBN = dbBooks[i].ISBN,
                    Title = dbBooks[i].Title,
                    Author = dbBooks[i].Author,
                    Year = dbBooks[i].Year,
                    Pages = dbBooks[i].Pages,
                    Description = dbBooks[i].Description,
                    Price = dbBooks[i].Price,
                    Discount = dbBooks[i].Discount,
                    GenreId = dbBooks[i].GenreId,
                    SupplierId = dbBooks[i].SupplierId,
                    Created = dbBooks[i].Created
                });
            }

            return books;
        }
    }
}
