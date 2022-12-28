using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WestCoastEdu.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View("index");
    }

    public IActionResult Courses()
    {
        return View("Courses");
    }


}
