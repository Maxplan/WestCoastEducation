using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WestCoastEdu.web.ViewModels
{
    public class CourseUpdateViewModel
    {

    [Required(ErrorMessage = "A Course ID Is Required")]
    [DisplayName("Course ID")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "A Course Name Is Required")]
    [DisplayName("Course Name")]
    public string CourseName { get; set; } = "";

    [Required(ErrorMessage = "A Course Title Is Required")]
    [DisplayName("Course Title")]
    public string CourseTitle { get; set; } = "";

    [Required(ErrorMessage = "A Start Date Is Required")]
    [DisplayName("Start Date")]
    public string StartDate { get; set; } = "";

    [Required(ErrorMessage = "A End Date Is Required")]
    [DisplayName("End Date")]
    public string EndDate { get; set; } = "";

    [Required(ErrorMessage = "A Course Desciption Is Required")]
    [DisplayName("Course Description")]
    public string CourseDescription { get; set; } = "";
    }
}