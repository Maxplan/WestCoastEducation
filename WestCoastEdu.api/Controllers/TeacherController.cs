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
            
            if(result is null) return BadRequest("Could Not Load List, List is empty");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TeacherPostViewModel model)
        {
            var exists = await _context.Teachers.SingleOrDefaultAsync(c => c.Email == model.Email);

            if(exists is not null) return BadRequest("Email Already Exists");

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
            if(await _context.SaveChangesAsync() > 0)
            {
                return Ok(); 
            }
            return BadRequest("Something went wrong when trying to save changes");
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id){
            var teacher = await _context.Teachers
            .Include(t => t.Competences)
            .Include(co => co.Courses)
            .FirstOrDefaultAsync(t => t.Id == id);
            if(teacher is null) return BadRequest("Teacher could not be found");
            var result = new
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Competences = teacher.Competences.Select(
                        c => new 
                        { 
                            Name = c.Name
                        }),
                        Courses = teacher.Courses.Select(
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
            var teacher = await _context.Teachers
            .Include(t => t.Competences)
            .Include(co => co.Courses)
            .FirstOrDefaultAsync(t => t.Email == email.ToLower().Trim());
            if(teacher is null) return BadRequest("Teacher could not be found");
            var result = new
                    {
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName,
                        Competences = teacher.Competences.Select(
                            c => new 
                            { 
                                Name = c.Name
                            }),
                            Courses = teacher.Courses.Select(
                                co => new
                                {
                                    Title = co.Title,
                                    CourseNumber = co.CourseNumber
                                })
                        
                    };
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, TeacherUpdateViewModel model)
        {
            var t = await _context.Teachers.FindAsync(id);
            
            if(t is null) return BadRequest($"Could not find a course with Id: {id}");

            t.DateOfBirth = model.DateOfBirth;
            t.FirstName = model.FirstName;
            t.LastName = model.LastName;
            t.Email = model.Email;
            t.Phone = model.Phone;
            t.Address = model.Address;
            t.PostalCode = model.PostalCode;
            t.City = model.City;
            t.Country = model.Country;


            _context.Update(t);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest("Something went wrong when saving changes");
        }

        [HttpPatch("addcompetence/{teacherId}/{competenceId}")]
        public async Task<IActionResult> AddCompetence(int teacherId, int competenceId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);
            if(teacher is null) return BadRequest($"Could not find a Teacher with Id:{teacherId}");

            var competence = await _context.Competences.FindAsync(competenceId);
            if(competence is null) return BadRequest($"Could not find a competence with Id:{competenceId}");

            teacher.Competences.Add(competence);
            if(await _context.SaveChangesAsync() > 0){
                return Ok($"Competence: {competenceId} was added to Teacher: {teacherId}");
            }
            return BadRequest("Something went wrong when trying to save changes");
        }
    }
}