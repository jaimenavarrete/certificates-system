using AutoMapper;
using CertificatesSystem.Models.Data.Identity;
using CertificatesSystem.Models.Exceptions;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesSystem.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        
        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _usersService.GetUsers();
            var roles = await _usersService.GetRoles();
         
            var usersViewModel = _mapper.Map<List<UserViewModel>>(users);
            var rolesViewModel = _mapper.Map<List<RoleViewModel>>(roles);

            foreach(var user in usersViewModel)
            {
                user.LockedOut = user.LockoutEnd > DateTime.UtcNow;
                user.Role = await _usersService.GetUserRoleById(user.Id ?? "");
            }

            UsersListViewModel viewModel = new()
            {
                UsersList = usersViewModel,
                Roles = rolesViewModel
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UsersListViewModel viewModel)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(viewModel);

                var succeeded = await _usersService.InsertUser(user, viewModel.Password, viewModel.Role);

                if (succeeded)
                    TempData["success"] = "El usuario se ha creado correctamente.";

                return RedirectToAction("Index");
            }
            catch (LogicException ex)
            {
                TempData["error"] = ex.Message;

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserLockState([FromForm] string userId)
        {
            try
            {
                var succeeded = await _usersService.ChangeUserLockState(userId);

                if (succeeded)
                    TempData["success"] = "El estado de bloqueo del usuario ha sido cambiado correctamente.";

                return RedirectToAction("Index");
            }
            catch (LogicException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromForm] string userId)
        {
            try
            {
                var succeeded = await _usersService.DeleteUser(userId);

                if (succeeded)
                    TempData["success"] = "El usuario ha sido borrado correctamente.";

                return RedirectToAction("Index");
            }
            catch (LogicException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}