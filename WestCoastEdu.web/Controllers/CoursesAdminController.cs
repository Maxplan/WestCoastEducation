using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Interfaces;
using WestCoastEdu.web.Models;
using WestCoastEdu.web.ViewModels;

namespace WestCoastEdu.web.Controllers;

[Route("courses/admin")]
public class CoursesAdminController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CoursesAdminController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    
    }
    public async Task<IActionResult> Index()
    {
        try
        {
            var courses = await _unitOfWork.CourseRepository.ListAllAsync();

            var model = courses.Select(a => new CourseListViewModel{
                CourseId = a.CourseId,
                CourseTitle = a.CourseTitle,
                CourseName = a.CourseName,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                CourseDescription = a.CourseDescription
            }).ToList();

            return View("Index", model);
        }
        catch (Exception ex)
        {
            
            var error = new ErrorModel{
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }
    [HttpGet("create")]
    public IActionResult Create(){
        var course = new CoursePostViewModel();
        return View("Create", course);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CoursePostViewModel course){
        try
        {
            if (!ModelState.IsValid) return View("Create", course);

            var titleExists = await _unitOfWork.CourseRepository.FindByTitleAsync(course.CourseTitle);

            if (titleExists is not null)
            {
                var createError = new ErrorModel
                {
                    ErrorTitle = "Something Went Wrong When Trying To Create Account",
                    ErrorMessage = $"There's already an Account for ''{course.CourseTitle}''"
                };
                return View("_Error", createError);
            }

            var courseToAdd = new Course()
            {
                CourseTitle = course.CourseTitle,
                CourseName = course.CourseName,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                CourseDescription = course.CourseDescription
            };

            if(await _unitOfWork.CourseRepository.AddAsync(courseToAdd)){
                if(await _unitOfWork.Complete()){
                    return RedirectToAction(nameof(Index));
                }
            }

            var error = new ErrorModel
            {
                ErrorTitle = "Something went wrong when trying to create a new account",
                ErrorMessage = "IS WONG"
            };
            return View("_Error", error);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel
                {
                    ErrorTitle = "Something went wrong when trying to create a new account",
                    ErrorMessage = ex.Message
                };
                return View("_Error", error);
        }
    }

    [HttpGet("edit/{courseId}")]
    public async Task<IActionResult> Edit(int courseId)
    {
        try
        {    
            var result = await _unitOfWork.CourseRepository.FindByIdAsync(courseId);

            if(result is null){
                var error = new ErrorModel
                {
                    ErrorTitle = "Something went wrong when trying to fetch course",
                    ErrorMessage = $"There's not a course with ID: {courseId}"
                }; 
                return View("_Error", error);
            }

            var model = new CourseUpdateViewModel{
                CourseId = result.CourseId,
                CourseTitle = result.CourseTitle,
                CourseName = result.CourseName,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                CourseDescription = result.CourseDescription
            };

            return View("Edit", model);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel{
                ErrorTitle = "Something went wrong when trying to fetch the account",
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }

    [HttpPost("edit/{courseId}")]
    public async Task<IActionResult> Edit(int courseId, CourseUpdateViewModel model)
    {
        try
        {
            if(!ModelState.IsValid) return View("Edit", model);

            var courseToUpdate = await _unitOfWork.CourseRepository.FindByIdAsync(courseId);

            if(courseToUpdate is null) return RedirectToAction(nameof(Index));

            courseToUpdate.CourseTitle = model.CourseTitle;
            courseToUpdate.CourseName = model.CourseName;
            courseToUpdate.StartDate = model.StartDate;
            courseToUpdate.EndDate = model.EndDate;
            courseToUpdate.CourseDescription = model.CourseDescription;

            if(await _unitOfWork.CourseRepository.UpdateAsync(courseToUpdate)){
                if(await _unitOfWork.Complete()){
                    return RedirectToAction(nameof(Index));
                }
            };
            var updateError = new ErrorModel{
                ErrorTitle = "Something went wrong",
                ErrorMessage = "Something went wrong when trying to update information"
            };
            return View("_Error", updateError);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel{
                ErrorTitle = "something went wrong when trying to save your changes",
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }

    [Route("delete/{courseId}")]
    public async Task<IActionResult> Delete(int courseId)
    {
        try
        {
            var courseToDelete = await _unitOfWork.CourseRepository.FindByIdAsync(courseId);

            if(courseToDelete is null) return RedirectToAction(nameof(Index));

            if(await _unitOfWork.CourseRepository.DeleteAsync(courseToDelete)){
                if(await _unitOfWork.Complete()){
                    return RedirectToAction(nameof(Index));
                }
            }

            var deleteError = new ErrorModel{
                ErrorTitle = "Something went wrong",
                ErrorMessage = "Something went wrong when trying to delete course"
            };

            return View("_Error", deleteError);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel{
                ErrorTitle = "Something went wrong when trying to delete course",
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }
}
