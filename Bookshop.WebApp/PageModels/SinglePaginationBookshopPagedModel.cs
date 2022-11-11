using AspNetCoreHero.ToastNotification.Abstractions;
using Bookshop.Contracts.Generics;

namespace Bookshop.WebApp.PageModels
{
    public class SinglePaginationBookshopPagedModel<T> : BookshopPageModel where T : class
    {
        private Paged<T> _paged;
        private int _currentPage;

        public Paged<T> Paged { get => _paged; }
        protected int CurrentPage { get => _currentPage; }

        public int NextPage
        {
            get
            {
                if (Paged.NextPageIsEmpty)
                {
                    return CurrentPage;
                }

                return CurrentPage + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                var previousPage = CurrentPage - 1;

                if (previousPage < 1)
                {
                    return CurrentPage;
                }

                return previousPage;
            }
        }

        public SinglePaginationBookshopPagedModel(INotyfService notyfService) 
            : 
            base(notyfService)
        {
        }

        public void SetPageItems(Paged<T> pagedItems, int page)
        {
            _currentPage = page;
            _paged = pagedItems;
        }
    }
}
