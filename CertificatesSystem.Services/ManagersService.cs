using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Services
{
    public class ManagersService : IManagersService
    {
        private readonly CertificatesSystemContext _context;
    
        public ManagersService(CertificatesSystemContext context)
        {
            _context = context;
        }
    
        public async Task<List<Manager>> GetAll() => await _context.Managers.ToListAsync();

        public async Task<Manager?> GetById(int id) => await _context.Managers.FindAsync(id);

        public async Task<bool> Create(Manager manager)
        {
            _context.Add(manager);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(Manager manager)
        {
            _context.Update(manager);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var manager = await GetById(id);
            _context.Remove(manager);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}