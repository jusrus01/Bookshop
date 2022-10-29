namespace Bookshop.Contracts.Generics
{
    public class Paged<T> where T : class
    {
        public Paged()
        {
        }

        public Paged(IList<T> items, int page, int pageSize)
        {
            Items = items.Count > pageSize ? items.SkipLast(1) : items;
            NextPageIsEmpty = items.Count < pageSize;
            PreviousPageIsEmpty = page <= 1;
            Count = items.Count != 0 ? items.Count - 1 : 0;
        }

        public int CurrentPage { get; set; }

        public IEnumerable<T> Items { get; set; }

        public bool NextPageIsEmpty { get; set; }

        public bool PreviousPageIsEmpty { get; set; }

        public int Count { get; set; }
    }
}
