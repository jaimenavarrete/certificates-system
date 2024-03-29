﻿using CertificatesSystem.Models.DataModels;

namespace CertificatesSystem.Models.Interfaces
{
    public interface IEnrollmentService
    {
        Task<Enrollment?> GetEnrolledStudent(int studentId, int year);

        Task<List<Enrollment>> GetEnrolledStudentsByGradeAndSection(int year, int gradeId, int sectionId);

        Task<bool> EnrollStudentsInGrade(Enrollment enrollment, string studentsNie);
    
        Task<bool> EnrollStudentsInGradeByPdf(Enrollment enrollment, string[] studentsInfo);

        Task<bool> RemoveEnrolledStudent(int[] studentsId, int year);
    }
}