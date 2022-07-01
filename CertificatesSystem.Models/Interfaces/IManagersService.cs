using CertificatesSystem.Models.DataModels;

namespace CertificatesSystem.Models.Interfaces
{
    public interface IManagersService
    {
        Task<List<Manager>> GetAll();

        Task<Manager?> GetById(int id);
    
        Task<bool> Create(Manager manager);

        Task<bool> Update(Manager manager);

        Task<bool> Delete(int id);
    }
}