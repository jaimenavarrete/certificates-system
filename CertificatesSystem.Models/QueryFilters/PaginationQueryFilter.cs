namespace CertificatesSystem.Models.QueryFilters
{
    public class PaginationQueryFilter
    {
        public PaginationQueryFilter(int rowsAmount, int currentPage)
        {
            RowsAmount = rowsAmount;
            NumberOfPages = (int) Math.Ceiling((decimal) RowsAmount / RowsPerPage);
            
            CurrentPage = currentPage;
            StartPosition = RowsPerPage * (CurrentPage - 1) + 1;
        }
        
        public int RowsPerPage => 25;

        public int RowsAmount { get; }

        public int NumberOfPages { get; }

        public int CurrentPage { get; }
        
        public int StartPosition { get; }

        public bool IsFirstPage => CurrentPage == 1;

        public bool IsLastPage => CurrentPage == NumberOfPages;
    }
}