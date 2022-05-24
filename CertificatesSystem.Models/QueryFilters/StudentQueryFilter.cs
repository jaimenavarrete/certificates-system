namespace CertificatesSystem.Models.QueryFilters;

public class StudentQueryFilter
{
    public string SearchText { get; set; }
    
    public OrderBy OrderBy { get; set; }
}

public enum OrderBy
{
    DateAsc = 1,
    DateDesc = 2,
    PopularityAsc = 3,
    PopularityDesc = 4
}