using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.api.Data;
using WestCoastEdu.api.Models;
using WestCoastEdu.api.ViewModels;

namespace WestCoastEdu.api.Controllers
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly WestCoastEduContext _context;
        public TeacherController(WestCoastEduContext context)
        {
            _context = context;
        }

        [HttpGet("listall")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Teachers
                .Select(
                    t => new
                    {
                        Id = t.Id,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        Competences = t.Competences.Select(
                            comp => new
                            {
                                Name = comp.Name
                            }
                        ),
                        Courses = t.Courses.Select(
                                c => new
                                {
                                    Title = c.Title,
                                    CourseNumber = c.CourseNumber
                                })
                    }
                ).ToListAsync();

            if (result is null) return StatusCode(500, "Internal Server error");

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TeacherPostViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var exists = await _context.Teachers.SingleOrDefaultAsync(c => c.Email == model.Email);

            if (exists is not null) return BadRequest("Email Already Exists");

            var teacher = new Teacher
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

            await _context.Teachers.AddAsync(teacher);

            if (await _context.SaveChangesAsync() > 0) return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _context.Teachers
            .Include(t => t.Competences)
            .Include(t => t.Courses)
            .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher is null) return BadRequest("Teacher not found");

            var result = new
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Country = teacher.Country,
                City = teacher.City,
                PostalCode = teacher.PostalCode,
                Address = teacher.Address,
                Competences = teacher.Competences.Select(
                        t => new
                        {
                            Name = t.Name
                        }),
                Courses = teacher.Courses.Select(
                        t => new
                        {
                            Title = t.Title,
                            CourseNumber = t.CourseNumber
                        })

            };
            return Ok(result);
        }

        [HttpGet("getbyemail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var teacher = await _context.Teachers
            .Include(t => t.Competences)
            .Include(t => t.Courses)
            .FirstOrDefaultAsync(t => t.Email == email.ToLower().Trim());

            if (teacher is null) return BadRequest("Teacher not found");

            var result = new
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Competences = teacher.Competences.Select(
                            t => new
                            {
                                Name = t.Name
                            }),
                Courses = teacher.Courses.Select(
                                t => new
                                {
                                    Title = t.Title,
                                    CourseNumber = t.CourseNumber
                                })
            };

            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, TeacherUpdateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher is null) return BadRequest("Teacher not found");

            teacher.DateOfBirth = model.DateOfBirth;
            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.Email = model.Email;
            teacher.Phone = model.Phone;
            teacher.Address = model.Address;
            teacher.PostalCode = model.PostalCode;
            teacher.City = model.City;
            teacher.Country = model.Country;

            _context.Update(teacher);

            if (await _context.SaveChangesAsync() > 0) return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpPatch("addcompetence/{teacherId}/{competenceId}")]
        public async Task<IActionResult> AddCompetence(int teacherId, int competenceId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);

            if (teacher is null) return BadRequest("Teacher not found");

            var competence = await _context.Competences.FindAsync(competenceId);

            if (competence is null) return BadRequest("Competence not found");

            teacher.Competences.Add(competence);

            if (await _context.SaveChangesAsync() > 0) return Ok("Competence added successfully");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.Include(t => t.Courses).FirstOrDefaultAsync(t => t.Id == id);

            if (teacher is null) return BadRequest("Teacher not found");

            foreach (var competence in teacher.Competences.ToList())
            {
                competence.Teachers.Remove(teacher);
                _context.Competences.Update(competence);
            }

            foreach (var course in teacher.Courses.ToList())
            {
                course.Teacher = null;
                _context.Courses.Update(course);
            }

            _context.Teachers.Update(teacher);
            _context.Teachers.Remove(teacher);

            if (await _context.SaveChangesAsync() > 0) return Ok("Teacher Successfully Removed");

            return StatusCode(500, "An error occurred while trying to save changes");
        }

        [HttpPatch("removecompetence/{teacherId}/{competenceId}")]
        public async Task<IActionResult> RemoveCompetence(int teacherId, int competenceId)
        {
            var teacher = await _context.Teachers
            .Include(t => t.Competences)
            .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null) return NotFound("Teacher not found");

            var competence = await _context.Competences.FindAsync(competenceId);

            if (competence == null) return NotFound("Competence not found");

            teacher.Competences.Remove(competence);

            _context.Teachers.Update(teacher);

            if (await _context.SaveChangesAsync() > 0) return Ok("Competence Successfully Removed");

            return StatusCode(500, "An error occurred while trying to save changes");
        }
    }
}