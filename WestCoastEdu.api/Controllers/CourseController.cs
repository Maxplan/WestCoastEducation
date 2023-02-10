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

        [HttpPost("add")]
        public async Task<IActionResult> Add(CoursePostViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var exists = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == model.CourseNumber && c.StartDate == model.StartDate);

            if (exists is not null) return BadRequest("Course Already Exists");

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
            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course is null) return BadRequest("Course not found");

            var result = new
            {
                Title = course.Title,
                CourseNumber = course.CourseNumber,
                Students = course.Students.Select(
                            c => new
                            {
                                FirstName = c.FirstName,
                                LastName = c.LastName
                            })
            };

            return Ok(result);
        }

        [HttpPatch("addteacher/{courseId}/{teacherId}")]
        public async Task<IActionResult> AddTeacherToCourse(int courseId, int teacherId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course is null) return BadRequest("Course not found");

            var teacher = await _context.Teachers.FindAsync(teacherId);

            if (teacher is null) return BadRequest("Teacher not found");

            course.Teacher = teacher;

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0) return Ok("Teacher successfully added");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, CourseUpdateViewModel model)
        {
            var courseToUpdate = await _context.Courses.FindAsync(id);

            if (courseToUpdate is null) return BadRequest($"Could not find a course with Id: {id}");
            courseToUpdate.Title = model.Title;
            courseToUpdate.CourseNumber = model.CourseNumber;
            courseToUpdate.DurationWk = model.DurationWk;
            courseToUpdate.StartDate = model.StartDate;

            _context.Update(courseToUpdate);

            if (await _context.SaveChangesAsync() > 0) return CreatedAtAction(nameof(GetById), new { id = courseToUpdate.CourseId }, courseToUpdate);

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpPatch("markasfull/{id}")]
        public async Task<IActionResult> MarkAsFull(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            course.IsFull = true;

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0) return Ok("Course successfully marked as full");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpPatch("markascompleted/{id}")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course is null) return BadRequest("Course not found");

            course.Status = CourseStatusEnum.Completed;

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0) return Ok("Course successfully marked as completed");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course is null) return BadRequest("Course not found");

            _context.Courses.Remove(course);

            if (await _context.SaveChangesAsync() > 0) return Ok("Course successully deleted");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpPatch("removestudent/{courseId}/{studentId}")]
        public async Task<IActionResult> RemoveStudent(int courseId, int studentId)
        {
            var course = await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course is null) return BadRequest("Course not found");

            var student = await _context.Students.FindAsync(studentId);

            if (student is null) return BadRequest("Student not found");

            student.Courses.Remove(course);

            _context.Students.Update(student);

            _context.Courses.Update(course);

            if (await _context.SaveChangesAsync() > 0) return Ok("Student successfully removed");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpPatch("removeteacher/{courseId}")]
        public async Task<IActionResult> RemoveTeacher(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course is null) return BadRequest("Course not found");

            course.TeacherId = null;

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Teacher successfully removed");
            }
            return StatusCode(500, "An error occurred while trying to save changes");
        }
    }
}