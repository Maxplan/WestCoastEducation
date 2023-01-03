using Microsoft.AspNetCore.Mvc;

namespace WestCoastEdu.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
}
