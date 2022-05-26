using CertificatesSystem.Models.DataModels;

namespace CertificatesSystem.Models.Interfaces;

public interface IEnrollmentService
{
    Task<List<StudentGrade>> GetEnrolledStudentsByGradeAndSection(int year, int gradeId, int sectionId);
}