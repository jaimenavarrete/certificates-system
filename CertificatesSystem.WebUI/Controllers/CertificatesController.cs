using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Exceptions;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Services.Common;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Spire.Pdf.Exporting.XPS.Schema;

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

        var viewModel = new CertificateViewModel
        {
            CurrentYear = DateTime.Now.Year,
            ManagersList = managersViewModel
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(StudyCertificateFormViewModel model)
    {
        try
        {
            var studentViewModel = await GetStudentViewModel(model.Nie);
            var managerViewModel = await GetManagerViewModel(model.ManagerId);
            var enrollment = await GetEnrollment(studentViewModel.Id ?? 0, model.Year);
            var gradeViewModel = _mapper.Map<GradeViewModel>(enrollment.Grade);

            var viewModel = new StudyCertificateReportViewModel
            {
                Student = studentViewModel,
                AcademicPerformance = model.AcademicPerformance,
                Behaviour = model.Behaviour,
                Manager = managerViewModel,
                Grade = gradeViewModel,
                Section = enrollment.Section.Name,
                Year = model.Year,
                IsCurrentYear = model.Year >= DateTime.Now.Year,
                CurrentDateInLetters = DateService.Convert(DateTime.Now, false)
            };

            return new ViewAsPdf("Reports/StudyCertificate", viewModel);
        }
        catch (BusinessException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GradesCertificate()
    {
        var managers = await _managersService.GetAll();
        var managersViewModel = _mapper.Map<List<ManagerViewModel>>(managers);

        var viewModel = new CertificateViewModel
        {
            CurrentYear = DateTime.Now.Year,
            ManagersList = managersViewModel
        };
        
        return View("GradesCertificate", viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> GradesCertificate(GradesCertificateSearchFormViewModel model)
    {
        try
        {
            var studentViewModel = await GetStudentViewModel(model.Nie);
            var managerViewModel = await GetManagerViewModel(model.ManagerId);
            var enrollment = await GetEnrollment(studentViewModel.Id ?? 0, model.Year);
            var gradeViewModel = _mapper.Map<GradeViewModel>(enrollment.Grade);

            var viewModel = new GradesCertificateReportViewModel
            {
                Student = studentViewModel,
                Manager = managerViewModel,
                Grade = gradeViewModel,
                Section = enrollment.Section.Name,
                Year = model.Year,
                CurrentDateInLetters = DateService.Convert(DateTime.Now, false)
            };

            return new ViewAsPdf("Reports/GradesCertificate", viewModel);
        }
        catch (BusinessException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("GradesCertificate");
        }
    }

    private async Task<StudentViewModel> GetStudentViewModel(int nie)
    {
        if (nie == 0)
            throw new BusinessException("Debe ingresar un NIE.");
        
        var student = await _studentsService.GetByNie(nie);
        var studentViewModel = _mapper.Map<StudentViewModel>(student);

        if (student is null)
            throw new BusinessException("El NIE que ha colocado no se encuentra registrado.");

        return studentViewModel;
    }

    private async Task<ManagerViewModel> GetManagerViewModel(int managerId)
    {
        if (managerId == 0)
            throw new BusinessException("Debe ingresar un directivo.");
        
        var manager = await _managersService.GetById(managerId);
        var managerViewModel = _mapper.Map<ManagerViewModel>(manager);

        if (manager is null)
            throw new BusinessException("El directivo que ha colocado no se encuentra registrado.");

        return managerViewModel;
    }
    
    private async Task<Enrollment> GetEnrollment(int studentId, int year)
    {
        if (year == 0)
            throw new BusinessException("Debe ingresar el año de estudio para la constancia.");
        
        var enrollment = await _enrollmentService.GetEnrolledStudent(studentId, year);

        if (enrollment is null)
            throw new BusinessException("El estudiante no se encuentra matriculado en el año que ha ingresado.");

        return enrollment;
    }
}