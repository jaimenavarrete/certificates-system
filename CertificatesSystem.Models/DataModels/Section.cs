using System;
using System.Collections.Generic;

namespace CertificatesSystem.Models.DataModels
{
    public partial class Section
    {
        public Section()
        {
            StudentGrades = new HashSet<StudentGrade>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
    }
}
