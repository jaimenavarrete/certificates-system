﻿using System.Text;
using AutoMapper;
using CertificatesSystem.Models.DataModels;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Services.Common;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Spire.Pdf;

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
        var studentsListAViewModel = _mapper.Map<List<EnrolledStudentsListViewModel>>(studentsListA);
        
        var studentsListB = await _enrollmentService.GetEnrolledStudentsByGradeAndSection(year, grade, 2);
        var studentsListBViewModel = _mapper.Map<List<EnrolledStudentsListViewModel>>(studentsListB);
        
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
    public async Task<IActionResult> EnrollStudentsInGrade(EnrollmentFormViewModel viewModel)
    {
        var enrollment = _mapper.Map<Enrollment>(viewModel);

        var result = await _enrollmentService.EnrollStudentsInGrade(enrollment, viewModel.StudentsNie);

        if (result) TempData["Success"] = "Los estudiantes fueron matriculados con éxito.";

        return RedirectToAction("Index", new { year = enrollment.Year, grade = enrollment.GradeId });
    }
    
    [HttpPost]
    public async Task<IActionResult> EnrollStudentsInGradeByPdf(EnrollmentByPdfFormViewModel viewModel)
    {
        var enrollment = _mapper.Map<Enrollment>(viewModel);
        var studentsInfo = PdfService.ExtractStudentsInfoFromPdf(viewModel.PdfDocument.OpenReadStream());

        var result = await _enrollmentService.EnrollStudentsInGradeByPdf(enrollment, studentsInfo);

        if (result) TempData["Success"] = "Los estudiantes fueron matriculados con éxito.";
        
        return RedirectToAction("Index", new { year = enrollment.Year, grade = enrollment.GradeId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveEnrolledStudent(EnrollmentRemoveFormViewModel viewModel)
    {
        var result = await _enrollmentService.RemoveEnrolledStudent(viewModel.StudentsId, viewModel.Year);
        
        if (result) TempData["Success"] = "El estudiante fue quitado de la matrícula con éxito.";

        return RedirectToAction("Index", new { year = viewModel.Year, grade = viewModel.GradeId });
    }
}