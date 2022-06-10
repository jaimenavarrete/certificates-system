namespace CertificatesSystem.WebUI.ViewModels;

public class CertificateViewModel
{
    public int CurrentYear { get; set; }

    public List<ManagerViewModel> ManagersList { get; set; }
}

public class StudyCertificateFormViewModel
{
    public int Nie { get; set; }

    public string AcademicPerformance { get; set; }

    public string Behaviour { get; set; }
    public int Year { get; set; }
    public int ManagerId { get; set; }
}

public class StudyCertificateReportViewModel
{
    public StudentViewModel Student { get; set; }
    public string AcademicPerformance { get; set; }
    public string Behaviour { get; set; }
    public ManagerViewModel Manager { get; set; }
    public GradeViewModel Grade { get; set; }
    public string Section { get; set; }
    public bool IsCurrentYear { get; set; }
    public int Year { get; set; }
    public string CurrentDateInLetters { get; set; }
}

public class GradesCertificateSearchFormViewModel
{
    public int Nie { get; set; }
    public int Year { get; set; }
    public int ManagerId { get; set; }
}

public class GradesCertificateReportViewModel
{
    public StudentViewModel Student { get; set; }
    public ManagerViewModel Manager { get; set; }
    public GradeViewModel Grade { get; set; }
    public string Section { get; set; }
    public int Year { get; set; }
    public string CurrentDateInLetters { get; set; }
}

public class SubjectViewModel
{
    public string Name { get; set; }

    public int Mark { get; set; }

    public string MarkInLetters { get; set; }

    public bool IsApproved { get; set; }
}