using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Models.QueryFilters;
using CertificatesSystem.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Services;

public class StudentsService : IStudentsService
{
    private readonly CertificatesSystemContext _context;
    
    public StudentsService(CertificatesSystemContext context)
    {
        _context = context;
    }
    
    public async Task<List<Student>> GetAll() => await _context.Students.ToListAsync();

    public Task<Student> GetSearch(StudentQueryFilter filters)
    {
        throw new NotImplementedException();
    }

    public async Task<Student?> GetByNie(int nie) => await _context.Students.FindAsync(nie);

    public async Task<string> GetPhotoByNie(int nie)
    {
        var student = await GetByNie(nie);
        var base64Photo = await PhotoService.GetPhotoAsBase64(student.PhotoId);
        
        return base64Photo;
    }

    public async Task<bool> Create(Student student, string photoBase64)
    {
        var photoId = await PhotoService.SavePhotoAsFile(photoBase64);
        student.PhotoId = photoId;
        
        _context.Add(student);
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }

    public async Task<bool> Update(Student newStudentInfo, int lastNie, string photoBase64)
    {
        var result = await Delete(lastNie);
        result = await Create(newStudentInfo, photoBase64);

        return result;
    }

    public async Task<bool> Delete(int nie)
    {
        var student = await GetByNie(nie);
        _context.Remove(student);
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
}