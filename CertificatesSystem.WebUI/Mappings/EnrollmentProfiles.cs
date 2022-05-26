using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings;

public class EnrollmentMapping : Profile
{
    public EnrollmentMapping()
    {
        CreateMap<StudentGrade, EnrollViewModel>();
        CreateMap<EnrollViewModel, StudentGrade>();
    }
}