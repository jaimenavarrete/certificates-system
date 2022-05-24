using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Rotativa.AspNetCore;

namespace CertificatesSystem.WebUI.Controllers;

public class ReportsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return new ViewAsPdf();
    }
}