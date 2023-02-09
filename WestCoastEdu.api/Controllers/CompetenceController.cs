using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.api.Data;
using WestCoastEdu.api.Models;
using WestCoastEdu.api.ViewModels;

namespace WestCoastEdu.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetenceController : ControllerBase
    {
        private readonly WestCoastEduContext _context;
        public CompetenceController(WestCoastEduContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Add(CompetencePostViewModel model)
        {
            var exists = await _context.Competences.SingleOrDefaultAsync(c => c.Name == model.Name);

            if(exists is not null) return BadRequest($"{model.Name} already exists in the database");

            var competence = new Competence
            {
                Name = model.Name
            };

            await _context.Competences.AddAsync(competence);
            if(await _context.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Add), new { Id = competence.Id }, new
                {
                    Id = competence.Id,
                    Name = competence.Name,
                });
            }
            return BadRequest("Something went wrong when trying to save changes");
        }
    }
}