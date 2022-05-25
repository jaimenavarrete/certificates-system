namespace CertificatesSystem.WebUI.ViewModels;

public class EnrollmentViewModel
{
    public int CurrentYear { get; set; }

    public int SelectedYear { get; set; }
    
    public List<GradeViewModel> GradesList { get; set; }
    
    public EnrollmentFormViewModel EnrollmentForm { get; set; }
}

public class EnrollmentFormViewModel
{
    public int Year { get; set; }
    
    public int Grade { get; set; }
}