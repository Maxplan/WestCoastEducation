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

            if(result is null)return BadRequest("Listing Students Failed. List is empty");
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(StudentPostViewModel model)
        {
            var exists = await _context.Students.SingleOrDefaultAsync(s => s.Email == model.Email);
            if(exists is not null) return BadRequest($"A Student with Email: {model.Email} Already Exists");
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
            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest("Could Not Save Student");
        }
        [HttpPatch()]
        public async Task<IActionResult> AddStudentToCourse(int courseId, int studentId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course is null) return NotFound($"A Course with Id: {courseId} could not be found");

            var student = await _context.Students.FindAsync(studentId);
            if (student is null) return NotFound($"A Student with Id: {studentId} could not be found");

            course.Students.Add(student);

            _context.Update(course);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest("Could not save changes");
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id){
            var student = await _context.Students
            .Include(co => co.Courses)
            .FirstOrDefaultAsync(t => t.Id == id);
            if(student is null) return BadRequest("Student could not be found");
            var result = new
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Courses = student.Courses.Select(
                        co => new
                        {
                            Title = co.Title,
                            CourseNumber = co.CourseNumber
                        })  
                };
            return Ok(result);
        }
        
        [HttpGet("getbyssn/{ssn}")]
        public async Task<IActionResult> GetBySsn(DateTime ssn){ // !! Lägg till ssn property och ta bort datetime
            var student = await _context.Students
            .Include(co => co.Courses)
            .FirstOrDefaultAsync(t => t.DateOfBirth.Date == ssn.Date);
            if(student is null) return BadRequest("Student could not be found");
            var result = new
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Courses = student.Courses.Select(
                        co => new
                        {
                            Title = co.Title,
                            CourseNumber = co.CourseNumber
                        })  
                };
            return Ok(result);
        }
    
        [HttpGet("getbyemail/{email}")]
        public async Task<IActionResult> GetByEmail(string email){
            var student = await _context.Students
            .Include(co => co.Courses)
            .FirstOrDefaultAsync(t => t.Email == email.ToLower().Trim());
            if(student is null) return BadRequest("Student could not be found");
            var result = new
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Courses = student.Courses.Select(
                        co => new
                        {
                            Title = co.Title,
                            CourseNumber = co.CourseNumber
                        })
                    
                };
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, StudentUpdateViewModel model)
        {
            var s = await _context.Students.FindAsync(id);
            
            if(s is null) return BadRequest($"Could not find a course with Id: {id}");

            s.DateOfBirth = model.DateOfBirth;
            s.FirstName = model.FirstName;
            s.LastName = model.LastName;
            s.Email = model.Email;
            s.Phone = model.Phone;
            s.Address = model.Address;
            s.PostalCode = model.PostalCode;
            s.City = model.City;
            s.Country = model.Country;
            
            _context.Update(s);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest("Something went wrong when saving changes");
        }
    }
}