using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Controllers;

[Route("courses/admin")]
public class CoursesAdminController : Controller
{
    private readonly WestCoastEduContext _context;

    public CoursesAdminController(WestCoastEduContext context)
    {
        _context = context;     
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.ToListAsync();
        return View("Index", courses);
    }

    [HttpGet("create")]
    public IActionResult Create(){
        var account = new Course();
        return View("Create", account);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Course account){
        await _context.Courses.AddAsync(account);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{courseId}")]
    public async Task<IActionResult> Edit(int courseId)
    {
        var account = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == courseId);

        if(account is not null) return View("Edit", account);

        return Content("Error...");
    }

    [HttpPost("edit/{courseId}")]
    public async Task<IActionResult> Edit(int courseId, Course account)
    {
        var courseToUpdate = _context.Courses.SingleOrDefault(c => c.CourseId == courseId);

        if(courseToUpdate is null) return RedirectToAction(nameof(Index));

        courseToUpdate.CourseName = account.CourseName;
        courseToUpdate.CourseTitle = account.CourseTitle;
        courseToUpdate.StartDate = account.StartDate;
        courseToUpdate.EndDate = account.EndDate;
        courseToUpdate.CourseDescription = account.CourseDescription;

        _context.Courses.Update(courseToUpdate);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [Route("delete/{courseId}")]
    public async Task<IActionResult> Delete(int courseId)
    {
        var courseToDelete = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == courseId);

        if(courseToDelete is null) return RedirectToAction(nameof(Index));

        _context.Courses.Remove(courseToDelete);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
