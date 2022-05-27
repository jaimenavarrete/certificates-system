using CertificatesSystem.Models.DataModels;

namespace CertificatesSystem.Models.Interfaces;

public interface IEnrollmentService
{
    Task<Enrollment> GetEnrolledStudent(int nie, int year);

    Task<List<Enrollment>> GetEnrolledStudentsByGradeAndSection(int year, int gradeId, int sectionId);

    Task<bool> EnrollStudentsInGrade(Enrollment enrollment, string studentsNie);

    Task<bool> RemoveEnrolledStudent(int nie, int year);
}