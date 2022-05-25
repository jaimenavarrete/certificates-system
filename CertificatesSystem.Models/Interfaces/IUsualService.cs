using CertificatesSystem.Models.DataModels;

namespace CertificatesSystem.Models.Interfaces;

public interface IUsualService
{
    Task<List<Grade>> GetGrades();
}