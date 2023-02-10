using System.ComponentModel.DataAnnotations;

namespace WestCoastEdu.api.ViewModels
{
    public class CoursePostViewModel
    {
        [Required(ErrorMessage = "Title is missing")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Course number is missing")]
        public int CourseNumber { get; set; }

        [Required(ErrorMessage = "Duration is missing")]
        public int DurationWk { get; set; }

        [Required(ErrorMessage = "Start date is missing")]
        public DateTime StartDate { get; set; }
    }
}