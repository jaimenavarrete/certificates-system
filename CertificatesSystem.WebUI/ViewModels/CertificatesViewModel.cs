namespace CertificatesSystem.WebUI.ViewModels
{
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

    public class GradesCertificateViewModel
    {
        public int CurrentYear { get; set; }

        public List<ManagerViewModel> ManagersList { get; set; }

        public int GradesCertificateType { get; set; }
    
        public GradesCertificateInfoViewModel GradesCertificateInfo { get; set; }
    }

    public class GradesCertificateInfoViewModel
    {
        public StudentViewModel Student { get; set; }
        public GradeViewModel Grade { get; set; }
        public SectionViewModel Section { get; set; }
        public int Year { get; set; }
    }

    public class GradesCertificateSearchFormViewModel
    {
        public int Nie { get; set; }
        public int Year { get; set; }
    }

    public class GradesCertificateFormViewModel
    {
        public int Nie { get; set; }
        public int Year { get; set; }
        public int ManagerId { get; set; }
        public int GradeId { get; set; }
        public string Section { get; set; }
        public string[] NamesBasics { get; set; }
        public int[] Basics { get; set; }
        public string[] NamesComplementary { get; set; }
        public string[] Complementary { get; set; }
        public string[] NamesCompetencies { get; set; }
        public string[] Competencies { get; set; }
    }

    public class GradesCertificateReportViewModel
    {
        public StudentViewModel Student { get; set; }
        public ManagerViewModel Manager { get; set; }
        public GradeViewModel Grade { get; set; }
    
        public string Section { get; set; }
        public int Year { get; set; }
        public string CurrentDateInLetters { get; set; }
    
        public List<SubjectViewModel> BasicsSubjects { get; set; }
    
        public List<SubjectViewModel> ComplementarySubjects { get; set; }
    
        public List<SubjectViewModel> CompetenciesSubjects { get; set; }
    }

    public class SubjectViewModel
    {
        public string Name { get; set; }

        public string Mark { get; set; }

        public string MarkInLetters { get; set; }

        public string IsApproved { get; set; }
    }
}