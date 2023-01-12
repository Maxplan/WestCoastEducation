using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Interfaces;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Repository;

public class CourseRepository : ICourseRepository
{
    private readonly WestCoastEduContext _context;
    public CourseRepository(WestCoastEduContext context){
        _context = context;
    }
    public async Task<bool> AddAsync(Course course)
    {
        try
        {
            await _context.Courses.AddAsync(course);
            return true;
        }
        catch (System.Exception)
        { 
            return false;
        }
    }
    public async Task<Course?> FindByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public Task<bool> DeleteAsync(Course course)
    {
        try
        {
            _context.Courses.Remove(course);
            return Task.FromResult(true);
        }
        catch (System.Exception)
        {
            return Task.FromResult(false);
        }
    }

    public async Task<Course?> FindByTitleAsync(string title)
    {
        return await _context.Courses.FindAsync(title);
    }

    public async Task<IList<Course>> ListAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            if (await _context.SaveChangesAsync() > 0) return true;
            return false; 
        }
        catch
        {
            return false;
        }
    }
    
    public Task<bool> UpdateAsync(Course course)
    {
        try
        {
            _context.Courses.Update(course);
            return Task.FromResult(true);
        }
        catch
        {  
            return Task.FromResult(false);
        }
    }
}
