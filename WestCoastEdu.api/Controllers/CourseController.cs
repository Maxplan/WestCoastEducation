using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.api.Data;
using WestCoastEdu.api.Models;
using WestCoastEdu.api.ViewModels;

namespace WestCoastEdu.api.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CourseController : ControllerBase
    {
        private readonly WestCoastEduContext _context;
        public CourseController(WestCoastEduContext context)
        {
            _context = context;
        }

        [HttpGet("listall")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Courses
                .Select(
                    c => new
                    {
                        Title = c.Title,
                        CourseNumber = c.CourseNumber,
                        Students = c.Students.Select(
                            s => new 
                            { 
                                FirstName = s.FirstName, 
                                LastName = s.LastName 
                            })
                    }
                ).ToListAsync();

            return Ok(result);
        }

        [HttpGet("getbystartdate/{startDate}")]
        public async Task<IActionResult> GetByStartDate(DateTime startDate)
        {
            var result = await _context.Courses
                .Where(c => c.StartDate.Date == startDate.Date)
                .Select(
                    c => new
                    {
                        Title = c.Title,
                        CourseNumber = c.CourseNumber,
                        Students = c.Students.Select(
                            s => new 
                            { 
                                FirstName = s.FirstName, 
                                LastName = s.LastName 
                            })
                    }
                ).ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CoursePostViewModel model)
        {
            var exists = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == model.CourseNumber && c.StartDate == model.StartDate);

            if(exists is not null) return BadRequest("Course Already Exists");

            var course = new Course
            {
                Title = model.Title,
                CourseNumber = model.CourseNumber,
                DurationWk = model.DurationWk,
                StartDate = model.StartDate,
                EndDate = model.StartDate.AddDays(model.DurationWk * 7)
            };
            
            await _context.Courses.AddAsync(course);
            if (await _context.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(GetById), new { Id = course.CourseId }, new
                {
                    Id = course.CourseId,
                    Title = course.Title,
                    CourseNumber = course.CourseNumber,
                    DurationWk = course.DurationWk,
                    StartDate = course.StartDate
                });
            }
            return BadRequest("Could not save changes");
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id){
            var result = await _context.Courses.FindAsync(id);
            var course = new{id = result.CourseId};
            return Ok(result);
        }
        [HttpPatch("addteacher")]
        public async Task<IActionResult> AddTeacherToCourse(int courseId, int teacherId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course is null) return NotFound("Something went wrong");

            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher is null) return NotFound("Something went wrong");

            course.Teacher = teacher;

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return StatusCode(500, "Crap!");
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, CourseUpdateViewModel model)
        {
            var courseToUpdate = await _context.Courses.FindAsync(id);
            
            if(courseToUpdate is null) return BadRequest($"Could not find a course with Id: {id}");
            courseToUpdate.Title = model.Title;
            courseToUpdate.CourseNumber = model.CourseNumber;
            courseToUpdate.DurationWk = model.DurationWk;
            courseToUpdate.StartDate = model.StartDate;

            _context.Update(courseToUpdate);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest("Something went wrong when saving changes");
        }

        [HttpPatch("markasfull/{id}")]
        public async Task<IActionResult> MarkAsFull(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            course.IsFull = true;

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest("Something went wrong when saving changes");
        }

        [HttpPatch("markascompleted/{id}")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            course.Status = CourseStatusEnum.Completed;

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok($"Course: {id} was successfully set to 'Completed'");
            }
            return BadRequest("Something went wrong when saving changes");
        }
    }
}