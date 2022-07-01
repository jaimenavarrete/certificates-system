using CertificatesSystem.Models.DataModels;

namespace CertificatesSystem.Models.Interfaces
{
    public interface IMiscellanyService
    {
        Task<List<Grade>> GetGrades();

        Task<Grade?> GetGradeById(int id);
    }
}