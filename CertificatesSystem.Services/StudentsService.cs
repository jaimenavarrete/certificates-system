using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Exceptions;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Models.QueryFilters;
using CertificatesSystem.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly CertificatesSystemContext _context;
    
        public StudentsService(CertificatesSystemContext context)
        {
            _context = context;
        }
    
        public async Task<List<Student>> GetAll() => await _context.Students.ToListAsync();

        public Task<Student> GetSearch(StudentQueryFilter filters) => throw new NotImplementedException();

        public async Task<Student?> GetById(int id) => await _context.Students.FindAsync(id);

        public async Task<Student?> GetByNie(int nie) => 
            await _context.Students.FirstOrDefaultAsync(s => s.Nie == nie);

        public async Task<string?> GetPhotoByNie(int nie)
        {
            var student = await GetByNie(nie);

            if (student is null) return null;

            var base64Photo = await PhotoService.GetPhotoAsBase64(student.PhotoId);
        
            return base64Photo;
        }

        public async Task<bool> Create(Student student, string photoBase64)
        {
            if (student is null) 
                throw new LogicException();

            if (student.Nie == 0 || string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Surname))
                throw new BusinessException("Debe rellenar todos los campos requeridos.");

            var studentByNie = await GetByNie(student.Nie);

            if (studentByNie is not null)
                throw new BusinessException("El NIE que ha ingresado ya pertenece a otro estudiante.");

            student.PhotoId = await PhotoService.SavePhotoAsFile(photoBase64);
            student.Name = MakeFirstWordLetterUppercase(student.Name);
            student.Surname = MakeFirstWordLetterUppercase(student.Surname);

            _context.Add(student);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(Student student, string photoBase64)
        {
            if (student is null) 
                throw new LogicException();

            if (student.Nie == 0 || string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Surname))
                throw new BusinessException("Debe rellenar todos los campos requeridos.");
        
            var oldStudent = await GetById(student.Id);

            if (oldStudent is null)
                throw new LogicException();

            var studentByNie = await GetByNie(student.Nie);
        
            if(studentByNie is not null && oldStudent.Id != studentByNie.Id)
                throw new BusinessException("El NIE que ha ingresado ya pertenece a otro estudiante."); 

            PhotoService.DeletePhoto(oldStudent.PhotoId);

            oldStudent.PhotoId = await PhotoService.SavePhotoAsFile(photoBase64);
            oldStudent.Nie = student.Nie;
            oldStudent.Name = MakeFirstWordLetterUppercase(student.Name);
            oldStudent.Surname = MakeFirstWordLetterUppercase(student.Surname);
            oldStudent.Birthdate = student.Birthdate;
            oldStudent.Address = student.Address;

            _context.Update(oldStudent);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var student = await GetById(id);

            if (student is null)
                throw new LogicException();
        
            PhotoService.DeletePhoto(student.PhotoId);

            _context.Remove(student);
            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
    
        public List<Student> ConvertStudentsInfoToStudentsList(string[] studentsInfo)
        {
            var studentList = new List<Student>();

            foreach (var studentInfo in studentsInfo)
            {
                var studentInfoArray = studentInfo.Split(" ");
                var nie = int.Parse(studentInfoArray[0]);
                var commaSeparatorPosition = Array.IndexOf(studentInfoArray, ",");

                var student = new Student
                {
                    Nie = nie
                };

                student.Name = studentInfoArray.Length > 1 ? string.Join(" ", studentInfoArray[(commaSeparatorPosition + 1)..]) : "[]";
                student.Surname = studentInfoArray.Length > 1 ? string.Join(" ", studentInfoArray[1..commaSeparatorPosition]) : "[]";

                studentList.Add(student);
            }

            return studentList;
        }
    
        private string MakeFirstWordLetterUppercase(string? sentence)
        {
            var lowerCaseSentence = sentence?.ToLower();
            var words = lowerCaseSentence?.Split(" ");

            if (words is null) return "Anónimo";

            for (var index = 0; index < words.Length; index++)
            {
                var firstLetterUpper = char.ToUpper(words[index][0]);

                if (words[index].Length == 1)
                {
                    words[index] =  firstLetterUpper.ToString();
                }
                else
                {
                    words[index] =  firstLetterUpper + words[index].ToLower()[1..];
                }
            }

            return string.Join(" ", words);
        }
    }
}