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
        var course = new Course();
        return View("Create", course);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Course course){
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{courseId}")]
    public async Task<IActionResult> Edit(int courseId)
    {
        var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == courseId);

        if(course is not null) return View("Edit", course);

        return Content("Error...");
    }

    [HttpPost("edit/{courseId}")]
    public async Task<IActionResult> Edit(int courseId, Course course)
    {
        var courseToUpdate = _context.Courses.SingleOrDefault(c => c.CourseId == courseId);

        if(courseToUpdate is null) return RedirectToAction(nameof(Index));

        courseToUpdate.CourseName = course.CourseName;
        courseToUpdate.CourseTitle = course.CourseTitle;
        courseToUpdate.StartDate = course.StartDate;
        courseToUpdate.EndDate = course.EndDate;
        courseToUpdate.CourseDescription = course.CourseDescription;

        _context.Courses.Update(courseToUpdate);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
