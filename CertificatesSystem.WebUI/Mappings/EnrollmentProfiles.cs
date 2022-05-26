using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings;

public class EnrollmentMapping : Profile
{
    public EnrollmentMapping()
    {
        CreateMap<Enrollment, EnrollViewModel>();
        CreateMap<EnrollViewModel, Enrollment>();
    }
}

public class EnrollmentFormMapping : Profile
{
    public EnrollmentFormMapping()
    {
        CreateMap<Enrollment, EnrollmentFormViewModel>();
        CreateMap<EnrollmentFormViewModel, Enrollment>();
    }
}