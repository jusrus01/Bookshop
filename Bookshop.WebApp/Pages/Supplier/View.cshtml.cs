using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookshop.WebApp.Pages.Supplier
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Administrator)]
    public class ViewModel : BookshopPageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        public ViewModel(ISupplierService supplierService, IMapper mapper, INotyfService notyfService) : base(notyfService)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        public SupplierViewModel _supplier { get; set; }
        public string _city { get; set; }
        public async Task<IActionResult> OnGetAsync(int supplierId)
        {
            var supplier = await _supplierService.GetSupplierById(supplierId);
            _supplier = _mapper.Map<SupplierViewModel>(supplier);
            var cities = await _supplierService.GetCitiesList();
            for (int i = 0; i < cities.Count; i++)
            {
                if (cities[i].Id == _supplier.CityId)
                {
                    _city = cities[i].Name;
                    break;
                }
            }
            return Page();

        }
    }
}
