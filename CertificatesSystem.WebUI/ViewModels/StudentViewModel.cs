namespace CertificatesSystem.WebUI.ViewModels
{
    public class StudentViewModel
    {
        public int? Id { get; set; }
    
        public int Nie { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    
        public DateTime? Birthdate { get; set; }

        public string Address { get; set; }

        public string? PhotoId { get; set; }
    }
}