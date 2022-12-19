using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Supplier
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Client, BookshopRoles.Administrator)]
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
        public int _sum { get; set; }
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

                var storages_by_id = await _supplierService.GetStoragesBySupplierId(supplierId);
                int sum = 0;
                foreach (var strg in storages_by_id)
                {
                    sum += strg.BookCount;
                }
            _sum = sum;
            return Page();

        }
    }
}
