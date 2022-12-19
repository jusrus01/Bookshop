using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bookshop.WebApp.PageModels;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Bookshop.WebApp.Pages.Supplier
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Administrator, BookshopRoles.Client)]
    public class AddModel : BookshopPageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        public AddModel(ISupplierService supplierService, IMapper mapper, INotyfService service) : base(service)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }
        [BindProperty]
        public SupplierViewModel supplier { get; set; }

        public List<SelectListItem> cities { get; set; }
        public List<Storage> storages { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var list = _supplierService.GetAllStorages();
            storages = _mapper.Map<List<Storage>>( _supplierService.GetAllStorages());
            var dbCities = await _supplierService.GetCitiesList();
            cities = new List<SelectListItem>();
            foreach (var c in dbCities)
            {
                cities.Add(new SelectListItem { Text = c.Name, Value= c.Id.ToString() });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int a, int b, int c)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                List<int> storageIdxArray = new List<int>();
                if (a > 0) storageIdxArray.Add(a);
                if (b > 0) storageIdxArray.Add(b);
                if (c > 0) storageIdxArray.Add(c);
                await _supplierService.AddAsync(_mapper.Map<SupplierDto>(supplier), storageIdxArray);
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return RedirectToPage("List");
        }
    }
}
