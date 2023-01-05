using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Controllers;

[Route("courses")]
public class CoursesController : Controller
{
    private readonly WestCoastEduContext _context;
    public CoursesController(WestCoastEduContext context)
    {
            _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.ToListAsync();
        return await Task.FromResult(View("Index", courses));
    }
}

