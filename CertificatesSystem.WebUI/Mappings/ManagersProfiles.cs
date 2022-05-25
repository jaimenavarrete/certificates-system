using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings;

public class ManagerMapping : Profile
{
    public ManagerMapping()
    {
        CreateMap<Manager, ManagerViewModel>();
        CreateMap<ManagerViewModel, Manager>();
    }
}

public class ManagerFormMapping : Profile
{
    public ManagerFormMapping()
    {
        CreateMap<Manager, ManagerFormViewModel>();
        CreateMap<ManagerFormViewModel, Manager>();
    }
}