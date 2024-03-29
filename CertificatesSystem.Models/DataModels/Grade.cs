﻿namespace CertificatesSystem.Models.DataModels
{
    public partial class Grade
    {
        public Grade()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
