using Microsoft.AspNetCore.Mvc;

namespace WestCoastEdu.web.Controllers;

[Route("accountsadmin")]
public class AccountsAdminController : Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
}
