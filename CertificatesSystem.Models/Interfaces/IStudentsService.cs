using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.QueryFilters;

namespace CertificatesSystem.Models.Interfaces;

public interface IStudentsService
{
    Task<List<Student>> GetAll();

    Task<Student> GetSearch(StudentQueryFilter filters);

    Task<Student?> GetByNie(int nie);
    
    Task<bool> Create(Student student);

    Task<bool> Update(int lastNie, Student student);

    Task<bool> Delete(int nie);
}