using Microsoft.AspNetCore.Mvc;

namespace CertificatesSystem.WebUI.Controllers;

public class EnrollmentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}