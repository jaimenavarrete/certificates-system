using System.Security.Claims;
using CertificatesSystem.Models.Data.Identity;

namespace CertificatesSystem.Models.Interfaces
{
    public interface IUsersService
    {
        Task<string> GetLoggedUserId(ClaimsPrincipal principal);

        Task<ApplicationUser> GetUserById(string userId);

        Task<string> GetUserNameById(string userId);

        Task<string> GetUserRoleById(string userId);

        Task<bool> IsUserInAdministrationRole(string userId);

        Task<IEnumerable<ApplicationUser>> GetUsers();

        Task<IEnumerable<ApplicationRole>> GetRoles();

        Task<bool> InsertUser(ApplicationUser user, string password, string role);

        Task<bool> ChangeUserLockState(string userId);

        Task<bool> DeleteUser(string userId);
    }
}