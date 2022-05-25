using System.Diagnostics;
using AutoMapper;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesSystem.WebUI.Controllers;

public class EnrollmentController : Controller
{
    private readonly IUsualService _usualService;
    private readonly IMapper _mapper;

    public EnrollmentController(IUsualService usualService, IMapper mapper)
    {
        _usualService = usualService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index(int year)
    {
        var viewModel = new EnrollmentViewModel
        {
            CurrentYear = DateTime.Now.Year,
            SelectedYear = year,
            GradesList = new List<GradeViewModel>()
        };
        
        return View(viewModel);
    }
}