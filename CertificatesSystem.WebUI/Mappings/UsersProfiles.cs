using AutoMapper;
using CertificatesSystem.Models.Data.Identity;
using CertificatesSystem.WebUI.ViewModels;

namespace CertificatesSystem.WebUI.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();
        }
    }

    public class UsersListMapping : Profile
    {
        public UsersListMapping()
        {
            CreateMap<ApplicationUser, UsersListViewModel>();
            CreateMap<UsersListViewModel, ApplicationUser>();
        }
    }

    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<ApplicationRole, RoleViewModel>();
            CreateMap<RoleViewModel, ApplicationRole>();
        }
    }
}