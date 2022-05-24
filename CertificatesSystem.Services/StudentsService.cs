using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Models.QueryFilters;
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

    public async Task<bool> Create(Student student)
    {
        _context.Add(student);
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }

    public async Task<bool> Update(Student student)
    {
        _context.Update(student);
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }

    public async Task<bool> Delete(int nie)
    {
        var student = GetByNie(nie);
        _context.Remove(student);
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
}