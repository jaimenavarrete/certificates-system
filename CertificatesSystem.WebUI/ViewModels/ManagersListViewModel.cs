namespace CertificatesSystem.WebUI.ViewModels;

public class ManagersListViewModel
{
    public List<ManagerViewModel> ManagersList { get; set; }

    public ManagerFormViewModel ManagerForm { get; set; }
}

public class ManagerFormViewModel : ManagerViewModel
{
}