using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
