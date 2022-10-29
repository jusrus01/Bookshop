using AspNetCoreHero.ToastNotification.Abstractions;
using Bookshop.Contracts.Generics;

namespace Bookshop.WebApp.PageModels
{
    public class SinglePaginationBookshopPagedModel<T> : BookshopPageModel where T : class
    {
        public SinglePaginationBookshopPagedModel(INotyfService notyfService) 
            : 
            base(notyfService)
        {
        }

        protected int CurrentPage { get; set; }

        public virtual Paged<T> Paged { get; set; }

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
    }
}
