using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookshop.BusinessLogic.Services;
using Bookshop.Contracts.Services;
using AutoMapper;
using Bookshop.DataLayer;
using AspNetCoreHero.ToastNotification.Abstractions;
using Bookshop.WebApp.ViewModels.Suppliers;
using Bookshop.WebApp.PageModels;
using Bookshop.Contracts.Generics;

namespace Bookshop.WebApp.Pages.Supplier
{
    public class   
        ListModel : SinglePaginationBookshopPagedModel<SupplierViewModel>
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private const int PageSize = 4;

        public ListModel(
            ISupplierService supplierService,
            IMapper mapper,
            INotyfService notyfService)
            :
            base(notyfService)
        {
            _mapper = mapper;
            _supplierService = supplierService;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var suppliers = await _supplierService.GetSuppliersPagedAsync(pageNumber, pageSize: 4);

            SetPageItems(_mapper.Map<Paged<SupplierViewModel>>(suppliers), pageNumber);

            return Page();
        }

    }
}
