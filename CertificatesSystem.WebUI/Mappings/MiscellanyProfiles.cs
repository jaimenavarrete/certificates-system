using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings;

public class GradeMapping : Profile
{
    public GradeMapping()
    {
        CreateMap<Grade, GradeViewModel>();
        CreateMap<GradeViewModel, Grade>();
    }
}