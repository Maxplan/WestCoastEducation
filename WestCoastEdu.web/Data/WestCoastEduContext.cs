using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Data;

public class WestCoastEduContext : DbContext
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Account> Accounts => Set<Account>();
    public WestCoastEduContext(DbContextOptions options) : base(options)
    {
    }
}
