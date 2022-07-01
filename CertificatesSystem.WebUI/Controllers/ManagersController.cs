using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesSystem.WebUI.Controllers
{
    public class ManagersController : Controller
    {
        private readonly IManagersService _managersService;
        private readonly IMapper _mapper;

        public ManagersController(IManagersService managersService, IMapper mapper)
        {
            _managersService = managersService;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var managersList = await _managersService.GetAll();
            var managersListViewModel = _mapper.Map<List<ManagerViewModel>>(managersList);
        
            var viewModel = new ManagersListViewModel()
            {
                ManagersList = managersListViewModel
            };
        
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ManagerFormViewModel input)
        {
            var manager = _mapper.Map<Manager>(input);
        
            var result = await _managersService.Create(manager);

            if (result) TempData["Success"] = "El directivo se creó correctamente.";

            return RedirectToAction("Index");
        }
    
        [HttpPost]
        public async Task<IActionResult> Update(ManagerFormViewModel input)
        {
            var manager = await _managersService.GetById(input.Id ?? 0);
            manager.Name = input.Name;
            manager.Surname = input.Surname;
            manager.Role = input.Role;
            manager.Gender = input.Gender;

            var result = await _managersService.Update(manager);

            if (result) TempData["Success"] = "El directivo se editó correctamente.";

            return RedirectToAction("Index");
        }
    
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _managersService.Delete(id);

            if (result) TempData["Success"] = "El directivo se borró correctamente.";

            return RedirectToAction("Index");
        }
    }
}