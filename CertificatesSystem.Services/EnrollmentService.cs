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

    public async Task<bool> EnrollStudentsInGrade(Enrollment enrollment, string studentsNie)
    {
        var nies = ConvertNiesStringToIntArray(studentsNie);

        foreach (var nie in nies)
        {
            var student = await _studentsService.GetByNie(nie);
            var studentEnrollment = CreateANewEnrollment(enrollment, student.Id);
            _context.Add(studentEnrollment);
        }

        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
    
    public async Task<bool> EnrollStudentsInGradeByPdf(Enrollment enrollment, string[] studentsInfo)
    {
        var studentsList = _studentsService.ConvertStudentsInfoToStudentsList(studentsInfo);
        var success = true;
        
        foreach (var student in studentsList)
        {
            var studentByNie = await _studentsService.GetByNie(student.Nie);

            if (studentByNie is null)
            {
                var result = await _studentsService.Create(student, "");
                if (!result)
                {
                    success = false;
                    continue;
                }
                
                studentByNie = await _studentsService.GetByNie(student.Nie);
            }

            var studentEnrollment = CreateANewEnrollment(enrollment, studentByNie.Id);
            _context.Add(studentEnrollment);
        }
        
        var rows = await _context.SaveChangesAsync();
        if (rows == 0) success = false;

        return success;
    }

    public async Task<bool> RemoveEnrolledStudent(int[] studentsId, int year)
    {
        foreach (var studentId in studentsId)
        {
            var enrolledStudent = await GetEnrolledStudent(studentId, year);
            _context.Remove(enrolledStudent);
        }
        
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }

    private int[] ConvertNiesStringToIntArray(string stringNies)
    {
        var stringNiesWithCommas = stringNies.Replace(" ", "").Replace("\r\n", ",").Replace("\n", ",");
        var stringNiesArray = stringNiesWithCommas.Split(",");
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