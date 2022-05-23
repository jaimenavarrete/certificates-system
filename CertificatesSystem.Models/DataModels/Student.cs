using System;
using System.Collections.Generic;

namespace CertificatesSystem.Models.DataModels
{
    public partial class Student
    {
        public Student()
        {
            StudentGrades = new HashSet<StudentGrade>();
        }

        public int Nie { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
    }
}
