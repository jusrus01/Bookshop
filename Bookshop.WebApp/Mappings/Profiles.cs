﻿
using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Generics;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.ViewModels.Books;
using Bookshop.WebApp.ViewModels.Clients;
using Bookshop.WebApp.ViewModels.Orders;

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
            CreateMap<OrderViewModel, OrderDto>();
            CreateMap<BookViewModel, BookDto>();
            CreateMap<EditClientViewModel, EditClientDto>();
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
        }
    }
}
