using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course?> FindByTitleAsync(string title);
}
