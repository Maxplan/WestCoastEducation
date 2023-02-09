namespace WestCoastEdu.api.Models
{
    public class Teacher : Person
    {
        public ICollection<Course> Courses { get; set; }
        public ICollection<Competence> Competences { get; set; } = new List<Competence>();
    }
}