using WestCoastEdu.api.Models;

namespace WestCoastEdu.api.ViewModels
{
    public class StudentGetViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public StudentGetViewModel(Student student)
        {
            FirstName = student.FirstName;
            LastName = student.LastName;
            Email = student.Email;
            Phone = student.Phone;
        }
    }
}