namespace WestCoastEdu.web.Models;

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = "";
    public string CourseTitle { get; set; } = "";
    public string StartDate { get; set; } = "";
    public string EndDate { get; set; } = "";
    public string CourseDescription { get; set; } = "";
}
