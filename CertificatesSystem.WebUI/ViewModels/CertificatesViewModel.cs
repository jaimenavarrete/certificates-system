namespace CertificatesSystem.WebUI.ViewModels;

public class StudyCertificateViewModel
{
    public int CurrentYear { get; set; }

    public List<ManagerViewModel> ManagersList { get; set; }

    public StudyCertificateFormViewModel StudyCertificateForm { get; set; }
}

public class StudyCertificateFormViewModel
{
    public int Nie { get; set; }
    public int Year { get; set; }
    public int ManagerId { get; set; }
}

public class StudyCertificateReportViewModel
{
    public StudentViewModel Student { get; set; }
    public ManagerViewModel Manager { get; set; }
    public string Grade { get; set; }
    public string Section { get; set; }
    public bool IsCurrentYear { get; set; }
    public int Year { get; set; }
    public string CurrentDateInLetters { get; set; }
}