using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Demo;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.WebApp.Models.Users;
using Bookshop.WebApp.ViewModels.Demo;
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

            // Configure auto mapping from ViewModel to Dto
            // .aka so we would not need to manually do this:
            /*
                var dto = new Dto 
                {
                    PropertyName = ViewModel.PropertyName
                }
            */

            CreateMap<CreateDemoViewModel, CreateDemoDto>();
        }

        public void DtoToViewModelMappings()
        {
            #region Users

            #endregion
            
        }
    }
}
