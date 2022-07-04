using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.QueryFilters;

namespace CertificatesSystem.Models.Interfaces
{
    public interface IStudentsService
    {
        Task<List<Student>> GetAll(PaginationQueryFilter pagination);

        Task<int> GetStudentsAmount();

        Task<Student> GetSearch(int nie);

        Task<Student?> GetByNie(int nie);

        Task<string?> GetPhotoByNie(int nie);
    
        Task<bool> Create(Student student, string photoBase64);

        Task<bool> Update(Student student, string photoBase64);

        Task<bool> Delete(int id);

        public List<Student> ConvertStudentsInfoToStudentsList(string[] studentsInfo);
    }
}