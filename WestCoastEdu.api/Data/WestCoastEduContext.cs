using Microsoft.EntityFrameworkCore;
using WestCoastEdu.api.Models;

namespace WestCoastEdu.api.Data
{
    public class WestCoastEduContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public WestCoastEduContext(DbContextOptions options) : base(options)
        {
        }
    }
}