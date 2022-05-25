using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Services;

public class UsualService : IUsualService
{
    private readonly CertificatesSystemContext _context;

    public UsualService(CertificatesSystemContext context)
    {
        _context = context;
    }

    public async Task<List<Grade>> GetGrades() => await _context.Grades.ToListAsync();
}