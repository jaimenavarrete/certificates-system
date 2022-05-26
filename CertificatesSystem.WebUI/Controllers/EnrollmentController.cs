using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesSystem.WebUI.Controllers;

public class EnrollmentController : Controller
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly IMiscellanyService _miscellanyService;
    private readonly IMapper _mapper;

    public EnrollmentController(IEnrollmentService enrollmentService, IMiscellanyService miscellanyService, IMapper mapper)
    {
        _enrollmentService = enrollmentService;
        _miscellanyService = miscellanyService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int year, int grade)
    {
        var grades = await _miscellanyService.GetGrades();
        var gradesViewModel = _mapper.Map<List<GradeViewModel>>(grades);
        var selectedGradeViewModel = gradesViewModel.FirstOrDefault(g => g.Id == grade);

        var studentsListA = await _enrollmentService.GetEnrolledStudentsByGradeAndSection(year, grade, 1);
        var studentsListAViewModel = _mapper.Map<List<EnrollViewModel>>(studentsListA);
        
        var studentsListB = await _enrollmentService.GetEnrolledStudentsByGradeAndSection(year, grade, 2);
        var studentsListBViewModel = _mapper.Map<List<EnrollViewModel>>(studentsListB);
        
        var viewModel = new EnrollmentsViewModel
        {
            CurrentYear = DateTime.Now.Year,
            SelectedYear = year,
            SelectedGrade = selectedGradeViewModel,
            GradesList = gradesViewModel,
            StudentsListA = studentsListAViewModel,
            StudentsListB = studentsListBViewModel
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EnrollStudents(EnrollmentsViewModel viewModel)
    {
        var enrollment = _mapper.Map<Enrollment>(viewModel.EnrollmentForm);

        var result = await _enrollmentService.EnrollStudentsInGrade(enrollment, viewModel.EnrollmentForm.StudentsNie);

        if (result) TempData["Success"] = "Los estudiantes fueron matriculados con éxito.";

        return RedirectToAction("Index", new { year = enrollment.Year, grade = enrollment.GradeId });
    }
}