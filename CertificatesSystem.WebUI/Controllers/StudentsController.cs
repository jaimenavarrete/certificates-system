using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Exceptions;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Models.QueryFilters;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesSystem.WebUI.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;
    
        public StudentsController(IStudentsService studentsService, IMapper mapper)
        {
            _studentsService = studentsService;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<IActionResult> Index(int page)
        {
            try
            {
                var pagination = await GetPagination(page);
                var paginationViewModel = _mapper.Map<PaginationViewModel>(pagination);
                var studentsList = await _studentsService.GetAll(pagination);
                var studentsListViewModel = _mapper.Map<List<StudentViewModel>>(studentsList);

                var viewModel = new StudentsListViewModel
                {
                    StudentsList = studentsListViewModel,
                    Pagination = paginationViewModel
                };
        
                return View(viewModel);
            }
            catch(BusinessException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", new { page = 1 });
            }
        }

        private async Task<PaginationQueryFilter> GetPagination(int page)
        {
            var rowsAmount = await _studentsService.GetStudentsAmount();
            var pagination = new PaginationQueryFilter(rowsAmount, page);

            if (page > pagination.NumberOfPages || page < 1 )
                throw new BusinessException("La página a la que está accediendo no existe");

            return pagination;
        }

        [HttpGet]
        public async Task<StudentViewModel?> GetByNie(int nie)
        {
            var student = await _studentsService.GetByNie(nie);

            if (student is null) return null;
        
            student.PhotoId = await _studentsService.GetPhotoByNie(nie);

            var studentViewModel = _mapper.Map<StudentViewModel>(student);
        
            return studentViewModel;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentFormViewModel input)
        {
            try
            {
                var student = _mapper.Map<Student>(input);

                var result = await _studentsService.Create(student, input.Photo);

                if (result) TempData["Success"] = "El estudiante se creó correctamente.";

                return RedirectToAction("Index");
            }
            catch(BusinessException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", new { page = 1 });
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> Update(StudentFormViewModel input)
        {
            try
            {
                var student = _mapper.Map<Student>(input);

                var result = await _studentsService.Update(student, input.Photo);

                if (result) TempData["Success"] = "El estudiante se editó correctamente.";

                return RedirectToAction("Index", new { page = 1 });
            }
            catch(BusinessException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", new { page = 1 });
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentsService.Delete(id);

            if (result) TempData["Success"] = "El estudiante se borró correctamente.";

            return RedirectToAction("Index", new { page = 1 });
        }
    }
}