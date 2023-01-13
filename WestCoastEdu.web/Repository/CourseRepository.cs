using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Interfaces;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Repository;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(WestCoastEduContext context) : base(context){}
    public async Task<Course?> FindByTitleAsync(string title)
    {
        return await _context.Courses.SingleOrDefaultAsync(c => c.CourseTitle.Trim().ToLower() == title.Trim().ToLower());
    }
}
