using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesSystem.WebUI.Controllers;

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
    public async Task<IActionResult> Index()
    {
        var studentsList = await _studentsService.GetAll();
        var studentsListViewModel = _mapper.Map<List<StudentViewModel>>(studentsList);
        
        var viewModel = new StudentsListViewModel()
        {
            StudentsList = studentsListViewModel
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentFormViewModel input)
    {
        var student = _mapper.Map<Student>(input);
        
        var result = await _studentsService.Create(student);

        if (result) TempData["Success"] = "El estudiante se creó correctamente.";

        return RedirectToAction("Index");
    }
}