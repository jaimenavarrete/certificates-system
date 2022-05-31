namespace CertificatesSystem.WebUI.ViewModels;

public class StudentsListViewModel
{
    public List<StudentViewModel> StudentsList { get; set; }

    public StudentFormViewModel StudentForm { get; set; }
}

public class StudentFormViewModel : StudentViewModel
{
    public string Photo { get; set; }
}