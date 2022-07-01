namespace CertificatesSystem.WebUI.ViewModels
{
    public class ManagerViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Gender { get; set; } = null!;
    }
}