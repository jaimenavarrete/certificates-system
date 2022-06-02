using AutoMapper;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Services.Common;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace CertificatesSystem.WebUI.Controllers;

public class CertificatesController : Controller
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly IStudentsService _studentsService;
    private readonly IManagersService _managersService;
    private readonly IMapper _mapper;

    public CertificatesController(
        IStudentsService studentsService,
        IEnrollmentService enrollmentService,
        IManagersService managersService,
        IMapper mapper)
    {
        _studentsService = studentsService;
        _enrollmentService = enrollmentService;
        _managersService = managersService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var managers = await _managersService.GetAll();
        var managersViewModel = _mapper.Map<List<ManagerViewModel>>(managers);

        var viewModel = new StudyCertificateViewModel
        {
            CurrentYear = DateTime.Now.Year,
            ManagersList = managersViewModel
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(StudyCertificateFormViewModel model)
    {
        var student = await _studentsService.GetByNie(model.Nie);
        var studentViewModel = _mapper.Map<StudentViewModel>(student);
        
        var manager = await _managersService.GetById(model.ManagerId);
        var managerViewModel = _mapper.Map<ManagerViewModel>(manager);
        
        var enrollment = await _enrollmentService.GetEnrolledStudent(student.Id, model.Year);

        var viewModel = new StudyCertificateReportViewModel
        {
            Student = studentViewModel,
            AcademicPerformance = model.AcademicPerformance,
            Behaviour = model.Behaviour,
            Manager = managerViewModel,
            Grade = enrollment.Grade.Name,
            Section = enrollment.Section.Name,
            Year = model.Year,
            IsCurrentYear = model.Year >= DateTime.Now.Year,
            CurrentDateInLetters = DateService.Convert(DateTime.Now, false)
        };

        return new ViewAsPdf("Reports/Index", viewModel);
    }
}