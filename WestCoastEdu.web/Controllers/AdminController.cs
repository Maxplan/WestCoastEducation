using Microsoft.AspNetCore.Mvc;

namespace WestCoastEdu.web.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}