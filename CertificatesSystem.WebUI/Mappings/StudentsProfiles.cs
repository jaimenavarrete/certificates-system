using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings;

public class StudentMapping : Profile
{
    public StudentMapping()
    {
        CreateMap<Student, StudentViewModel>();
        CreateMap<StudentViewModel, Student>();
    }
}

public class StudentFormMapping : Profile
{
    public StudentFormMapping()
    {
        CreateMap<Student, StudentFormViewModel>();
        CreateMap<StudentFormViewModel, Student>();
    }
}