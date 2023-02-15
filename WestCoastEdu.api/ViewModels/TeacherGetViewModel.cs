using WestCoastEdu.api.Models;

namespace WestCoastEdu.api.ViewModels
{
    public class TeacherGetViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Competence> Competences { get; set; }
        public ICollection<CourseGetViewModel> Courses { get; set; }
        public TeacherGetViewModel(Teacher teacher)
        {
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Email = teacher.Email;
            Phone = teacher.Phone;
            Competences = teacher.Competences.Select(c => new Competence
            {
                Name = c.Name
            }).ToList();
            Courses = teacher.Courses.Select(c => new CourseGetViewModel(c)).ToList();
        }
    }
}