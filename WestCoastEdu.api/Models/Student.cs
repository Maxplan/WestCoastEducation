using System.ComponentModel.DataAnnotations.Schema;

namespace WestCoastEdu.api.Models
{
    public class Student : Person
    {
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}