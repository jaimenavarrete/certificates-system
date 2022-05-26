using CertificatesSystem.Models.DataModels;

namespace CertificatesSystem.Models.Interfaces;

public interface IEnrollmentService
{
    Task<List<Enrollment>> GetEnrolledStudentsByGradeAndSection(int year, int gradeId, int sectionId);

    Task<bool> EnrollStudentsInGrade(Enrollment enrollment, string studentsNie);
}