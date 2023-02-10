
using System.ComponentModel.DataAnnotations;

namespace WestCoastEdu.api.ViewModels
{
    public class TeacherPostViewModel
    {
        [Required(ErrorMessage = "Date of birth is missing")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Firstname is missing")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is missing")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is missing")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}