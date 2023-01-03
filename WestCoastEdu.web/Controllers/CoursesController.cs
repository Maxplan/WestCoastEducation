using Microsoft.AspNetCore.Mvc;

namespace WestCoastEdu.web.Controllers;

public class CoursesController : Controller
{
    public IActionResult Index()
    {
        return View("Courses");
    }
}
