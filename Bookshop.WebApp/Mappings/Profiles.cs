
using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.Generics;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.ViewModels.Books;
using Bookshop.WebApp.ViewModels.Clients;
using Bookshop.WebApp.ViewModels.Orders;
using Bookshop.WebApp.ViewModels.Suppliers;

namespace Bookshop.WebApp.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            EntityToDtoMappings();
            ViewModelToDtoMappings();
            DtoToViewModelMappings();
        }

        private void EntityToDtoMappings()
        {
            #region Clients
            CreateMap<ApplicationUser, ClientDto>();
            CreateMap<ApplicationRole, RoleDto>();
            #endregion
        }

        private void ViewModelToDtoMappings()
        {
            CreateMap<LoginViewModel, LoginDto>();
            CreateMap<RegisterViewModel, RegisterDto>();
            CreateMap<CreateOrderViewModel, OrderDto>();
            CreateMap<BookViewModel, BookDto>();
            CreateMap<EditClientViewModel, EditClientDto>();
            CreateMap<EditOrderViewModel, EditOrderDto>();
        }

        public void DtoToViewModelMappings()
        {
            CreateMap<ClientDto, EditClientViewModel>();
            CreateMap<ClientDto, ClientViewModel>();
            CreateMap<PartialClientDto, PartialClientViewModel>();
            CreateMap<Paged<PartialClientDto>, Paged<PartialClientViewModel>>();
            CreateMap<PartialBookDto, PartialBookViewModel>();
            CreateMap<BookDto, BookViewModel>();
            CreateMap<Paged<PartialBookDto>, Paged<PartialBookViewModel>>();
            CreateMap<Paged<PartialOrderDto>, Paged<PartialOrderViewModel>>();
            CreateMap<PartialOrderDto, PartialOrderViewModel>();
            CreateMap<Paged<Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto>, Paged<SupplierViewModel>>();
            CreateMap<Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto, Paged<SupplierViewModel>>();
            CreateMap<Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto, SupplierViewModel>();
            CreateMap<Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto, Supplier>();
            CreateMap<Supplier, Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto>();
            CreateMap<SupplierViewModel, Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto>();
            CreateMap<Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto, SupplierViewModel>();
            CreateMap<Bookshop.Contracts.DataTransferObjects.Suppliers.SupplierDto, Supplier>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, CreateOrderViewModel>();
            CreateMap<StorageDto, Storage>();
            CreateMap<Storage, StorageDto>();

        }
    }
}
