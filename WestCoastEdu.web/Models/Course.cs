namespace WestCoastEdu.web;
public class Course
{
    public int CourseId { get; init; }
    public string? CourseName { get; private set; }
    public string? StartDate { get; private set; }
    public string? EndDate { get; private set; }
    public string? CourseDescription { get; set; }
}
