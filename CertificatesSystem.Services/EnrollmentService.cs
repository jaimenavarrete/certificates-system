using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly CertificatesSystemContext _context;
    private readonly IStudentsService _studentsService;
    
    public EnrollmentService(CertificatesSystemContext context, IStudentsService studentsService)
    {
        _context = context;
        _studentsService = studentsService;
    }

    public async Task<Enrollment?> GetEnrolledStudent(int studentId, int year)
    {
        return await _context.Enrollments
            .Include(e => e.Grade)
            .Include(e => e.Section)
            .Where(e => e.StudentId == studentId && e.Year == year)
            .FirstOrDefaultAsync();
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
            var student = await _studentsService.GetByNie(nie);
            var studentEnrollment = CreateANewEnrollment(enrollment, student.Id);
            _context.Add(studentEnrollment);
        }

        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }

    public async Task<bool> RemoveEnrolledStudent(int studentId, int year)
    {
        var enrolledStudent = await GetEnrolledStudent(studentId, year);

        _context.Remove(enrolledStudent);
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

    private Enrollment CreateANewEnrollment(Enrollment enrollment, int studentId)
    {
        return new Enrollment()
        {
            StudentId = studentId,
            GradeId = enrollment.GradeId,
            SectionId = enrollment.SectionId,
            Year = enrollment.Year
        };
    }
}