
using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.Contracts.Generics;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.PageModels.Users;
using Bookshop.WebApp.ViewModels.Clients;
using Bookshop.WebApp.ViewModels.Users;

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
            #endregion
        }

        private void ViewModelToDtoMappings()
        {
            #region Users
            CreateMap<LoginViewModel, LoginDto>();
            CreateMap<RegisterViewModel, RegisterDto>();
            #endregion
        }

        public void DtoToViewModelMappings()
        {
            #region Clients
            CreateMap<ClientDto, ClientViewModel>();
            CreateMap<PartialClientDto, PartialClientViewModel>();
            CreateMap<Paged<PartialClientDto>, Paged<PartialClientViewModel>>();
            CreateMap<PartialBookDto, PartialBookViewModel>();
            CreateMap<Paged<PartialBookDto>, Paged<PartialBookViewModel>>();
            #endregion
        }
    }
}
