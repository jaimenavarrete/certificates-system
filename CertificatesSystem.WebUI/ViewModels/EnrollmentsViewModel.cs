using CertificatesSystem.WebUI.Controllers;

namespace CertificatesSystem.WebUI.ViewModels;

public class EnrollmentsViewModel
{
    public int CurrentYear { get; set; }

    public int SelectedYear { get; set; }

    public GradeViewModel SelectedGrade { get; set; }

    public List<GradeViewModel> GradesList { get; set; }
    
    public List<EnrolledStudentsListViewModel> StudentsListA { get; set; }
    
    public List<EnrolledStudentsListViewModel> StudentsListB { get; set; }
}

public class EnrollmentFormViewModel
{
    public int GradeId { get; set; }

    public int SectionId { get; set; }
    
    public int Year { get; set; }
    
    public string StudentsNie { get; set; }
}

public class EnrollmentRemoveFormViewModel
{
    public int[] StudentsId { get; set; }
    
    public int GradeId { get; set; }

    public int Year { get; set; }
}