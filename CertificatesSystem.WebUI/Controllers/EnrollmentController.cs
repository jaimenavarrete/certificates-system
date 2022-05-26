using AutoMapper;
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
        
        var viewModel = new EnrollmentViewModel
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
}