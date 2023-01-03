using Microsoft.AspNetCore.Mvc;

namespace WestCoastEdu.web.Controllers;

public class AdministrationController : Controller
{
    public IActionResult Index()
    {
        return View("Administration");
    }
    public IActionResult Error()
    {
        return View("Error");
    }
}
