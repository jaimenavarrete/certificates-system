namespace CertificatesSystem.WebUI.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }

        public int NumberOfPages { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }
    }
}