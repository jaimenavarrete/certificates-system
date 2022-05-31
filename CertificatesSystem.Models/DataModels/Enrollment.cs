using System;
using System.Collections.Generic;

namespace CertificatesSystem.Models.DataModels
{
    public partial class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GradeId { get; set; }
        public int SectionId { get; set; }
        public int Year { get; set; }

        public virtual Grade Grade { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
        public virtual Section Section { get; set; } = null!;
    }
}
