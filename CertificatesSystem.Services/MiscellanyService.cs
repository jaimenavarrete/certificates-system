using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Services
{
    public class MiscellanyService : IMiscellanyService
    {
        private readonly CertificatesSystemContext _context;

        public MiscellanyService(CertificatesSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Grade>> GetGrades() => await _context.Grades.ToListAsync();
        public async Task<Grade?> GetGradeById(int id) => await _context.Grades.FindAsync(id);
    }
}