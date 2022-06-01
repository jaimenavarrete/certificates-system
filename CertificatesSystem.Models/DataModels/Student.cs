namespace CertificatesSystem.Models.DataModels
{
    public partial class Student
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public int Nie { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Address { get; set; } = null!;
        public string? PhotoId { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
