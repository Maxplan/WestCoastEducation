using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.api.Data;
using WestCoastEdu.api.Models;
using WestCoastEdu.api.ViewModels;

namespace WestCoastEdu.api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly WestCoastEduContext _context;
        public StudentController(WestCoastEduContext context)
        {
            _context = context;
        }

        [HttpGet("listall")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Students
                .Select(
                    s => new
                    {
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Courses = s.Courses.Select(
                            c => new
                            {
                                Title = c.Title,
                                CourseNumber = c.CourseNumber
                            }
                        )
                    }
                ).ToListAsync();

            if (result is null) return StatusCode(500, "Internal server error");

            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(StudentPostViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var exists = await _context.Students.SingleOrDefaultAsync(s => s.Email == model.Email);

            if (exists is not null) return BadRequest("Email already exists");

            var student = new Student
            {
                DateOfBirth = model.DateOfBirth,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                PostalCode = model.PostalCode,
                City = model.City,
                Country = model.Country
            };

            await _context.Students.AddAsync(student);

            if (await _context.SaveChangesAsync() > 0) return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);

            return StatusCode(500, "An error occurred while trying to save changes");
        }
        [HttpPatch("addstudenttocourse/{studentId}/{courseId}")]
        public async Task<IActionResult> AddStudentToCourse(int studentId, int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course is null) return NotFound("Course not found");

            var student = await _context.Students.FindAsync(studentId);

            if (student is null) return NotFound("Student not found");

            course.Students.Add(student);

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0) return Ok("Student successfully added to course");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == id);

            if (student is null) return BadRequest("Student not found");

            var result = new
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Courses = student.Courses.Select(
                        s => new
                        {
                            Title = s.Title,
                            CourseNumber = s.CourseNumber
                        })
            };
            return Ok(result);
        }

        [HttpGet("getbyssn/{ssn}")]
        public async Task<IActionResult> GetBySsn(DateTime ssn)
        { // !! Lägg till ssn property och ta bort datetime
            var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.DateOfBirth.Date == ssn.Date);

            if (student is null) return BadRequest("Student not found");

            var result = new
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Courses = student.Courses.Select(
                        s => new
                        {
                            Title = s.Title,
                            CourseNumber = s.CourseNumber
                        })
            };
            return Ok(result);
        }

        [HttpGet("getbyemail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Email == email.ToLower().Trim());

            if (student is null) return BadRequest("Student not found");

            var result = new
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Courses = student.Courses.Select(
                        s => new
                        {
                            Title = s.Title,
                            CourseNumber = s.CourseNumber
                        })

            };
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, StudentUpdateViewModel model)
        {
            var student = await _context.Students.FindAsync(id);

            if (student is null) return BadRequest($"Could not find a course with Id: {id}");

            student.DateOfBirth = model.DateOfBirth;
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.Phone = model.Phone;
            student.Address = model.Address;
            student.PostalCode = model.PostalCode;
            student.City = model.City;
            student.Country = model.Country;

            _context.Update(student);

            if (await _context.SaveChangesAsync() > 0) return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);

            return BadRequest("Something went wrong when saving changes");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student is null) return BadRequest("Could not find Student");

            _context.Students.Remove(student);

            if (await _context.SaveChangesAsync() > 0) return Ok("Student Successully Deleted");

            return StatusCode(500, "An error occurred while trying to save changes");
        }
    }
}