using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookshop.WebApp.Pages.Supplier
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Administrator)]

    public class EditModel : BookshopPageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        public EditModel(ISupplierService supplierService, IMapper mapper, INotyfService service) : base(service)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }
        [BindProperty]
        public SupplierViewModel _supplier { get; set; }
        public List<SelectListItem> _cities { get; set; }
        public List<Storage> storages { get; set; }
        public List<Storage> storages_by_supplier { get; set; }
        public async Task<IActionResult> OnGet(int supplierId)
        {
            var list = _supplierService.GetAllStorages();
            var supplier = await _supplierService.GetSupplierById(supplierId);
            storages = _mapper.Map<List<Storage>>(_supplierService.GetAllStorages());
            var storagesById = await _supplierService.GetStoragesBySupplierId(supplierId);
            storages_by_supplier = new List<Storage>();
            foreach (var storage in storagesById)
            {
                storages_by_supplier.Add(_mapper.Map<Storage>(storage));
            }
            if (supplier == null) return RedirectToPage("/notFound");
            _supplier = _mapper.Map<SupplierViewModel>(supplier);

            var dbCities = await _supplierService.GetCitiesList();
            _cities = new List<SelectListItem>();
            foreach (var c in dbCities)
            {
                _cities.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
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
                await _supplierService.UpdateSupplierAsync(_mapper.Map<SupplierDto>(_supplier), storageIdxArray);
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return RedirectToPage("List");
        }
    }
}
