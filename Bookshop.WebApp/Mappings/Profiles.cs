using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.WebApp.PageModels.Users;
using Bookshop.WebApp.ViewModels.Clients;
using Bookshop.WebApp.ViewModels.Users;

namespace Bookshop.WebApp.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            ViewModelToDtoMappings();
            DtoToViewModelMappings();
        }

        public void ViewModelToDtoMappings()
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
            #endregion
        }
    }
}
