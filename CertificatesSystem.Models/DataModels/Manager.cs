namespace CertificatesSystem.Models.DataModels
{
    public partial class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Gender { get; set; } = null!;
    }
}
