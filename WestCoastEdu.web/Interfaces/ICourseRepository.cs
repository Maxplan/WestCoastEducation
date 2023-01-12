using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Interfaces;

public interface ICourseRepository
{
    Task<IList<Course>> ListAllAsync();
    Task<Course?> FindByTitleAsync(string title);
    Task<Course?> FindByIdAsync(int id);
    Task<bool> AddAsync(Course course);
    Task<bool> UpdateAsync(Course course);
    Task<bool> DeleteAsync(Course course);
    Task<bool> SaveAsync();
}
