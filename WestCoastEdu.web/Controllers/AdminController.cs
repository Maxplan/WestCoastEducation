using Microsoft.AspNetCore.Mvc;

namespace WestCoastEdu.web.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
    public IActionResult Error()
    {
        return View("Error");
    }
}
