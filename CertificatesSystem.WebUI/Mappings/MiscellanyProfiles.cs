using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings
{
    public class GradeMapping : Profile
    {
        public GradeMapping()
        {
            CreateMap<Grade, GradeViewModel>();
            CreateMap<GradeViewModel, Grade>();
        }
    }

    public class SectionMapping : Profile
    {
        public SectionMapping()
        {
            CreateMap<Section, SectionViewModel>();
            CreateMap<SectionViewModel, Section>();
        }
    }
}