using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly CertificatesSystemContext _context;
    
    public EnrollmentService(CertificatesSystemContext context)
    {
        _context = context;
    }
    
    public async Task<List<StudentGrade>> GetEnrolledStudentsByGradeAndSection(int year, int gradeId, int sectionId)
    {
        return await _context.StudentGrades
            .Include(e => e.Student)
            .Where(e => e.Year == year && e.GradeId == gradeId && e.SectionId == sectionId)
            .ToListAsync();
    }
}