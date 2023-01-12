using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WestCoastEdu.web.Models;

public class Course
{
    public int CourseId { get; set; }
    
    [DisplayName("Course Name")]
    public string CourseName { get; set; } = "";

    [DisplayName("Course Title")]
    public string CourseTitle { get; set; } = "";

    [DisplayName("Start Date")]
    public string StartDate { get; set; } = "";

    [DisplayName("End Date")]
    public string EndDate { get; set; } = "";

    [DisplayName("Course Description")]
    public string CourseDescription { get; set; } = "";
}
