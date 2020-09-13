namespace ClearSky.Web.Models
{
    public class PagedCollectionViewModel
    {
        public PagedCollectionViewModel(int currentPage, int pageCount)
        {
            CurrentPage = currentPage;
            PageCount = pageCount;

            if (CurrentPage > 1)
            {
                PreviousPage = CurrentPage - 1;
                HasPreviousPage = true;
            }
            if (CurrentPage < PageCount)
            {
                NextPage = CurrentPage + 1;
                HasNextPage = true;
            }
        }

        public int PreviousPage { get; }
        public int CurrentPage { get; }
        public int NextPage { get; }
        public int PageCount { get; }

        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }

    }
}
