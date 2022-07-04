using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.QueryFilters;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings
{
    public class PaginationMapping : Profile
    {
        public PaginationMapping()
        {
            CreateMap<PaginationQueryFilter, PaginationViewModel>();
            CreateMap<PaginationViewModel, PaginationQueryFilter>();
        }
    }
    
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