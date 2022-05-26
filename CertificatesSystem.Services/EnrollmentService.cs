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
    
    public async Task<List<Enrollment>> GetEnrolledStudentsByGradeAndSection(int year, int gradeId, int sectionId)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.Year == year && e.GradeId == gradeId && e.SectionId == sectionId)
            .ToListAsync();
    }

    public async Task<bool> EnrollStudentsInGrade(Enrollment enrollment, string studentsNieString)
    {
        var nies = ConvertNiesStringToIntArray(studentsNieString);

        foreach (var nie in nies)
        {
            var enrollStudent = CreateANewEnrollment(enrollment, nie);
            _context.Add(enrollStudent);
        }

        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }

    private int[] ConvertNiesStringToIntArray(string stringNies)
    {
        var stringNiesWithoutSpaces = stringNies.Replace(" ", "");
        var stringNiesArray = stringNiesWithoutSpaces.Split(",");
        var niesArray = Array.ConvertAll(stringNiesArray, int.Parse);

        return niesArray;
    }

    private Enrollment CreateANewEnrollment(Enrollment enrollment, int nie)
    {
        return new Enrollment()
        {
            Nie = nie,
            GradeId = enrollment.GradeId,
            SectionId = enrollment.SectionId,
            Year = enrollment.Year
        };
    }
}