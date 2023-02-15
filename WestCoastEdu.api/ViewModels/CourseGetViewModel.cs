using WestCoastEdu.api.Models;

namespace WestCoastEdu.api.ViewModels
{
    public class CourseGetViewModel
    {
        public string Title { get; set; }
        public int CourseNumber { get; set; }
        public int DurationWk { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }


        public ICollection<StudentGetViewModel> Students { get; set; }
        public CourseGetViewModel(Course course)
        {
            Title = course.Title;
            CourseNumber = course.CourseNumber;
            DurationWk = course.DurationWk;
            StartDate = course.StartDate.Date.ToString("yyyy-MM-dd");
            EndDate = course.StartDate.AddDays(course.DurationWk * 7).ToString("yyyy-MM-dd");

            DateTime currentDate = DateTime.UtcNow.Date;

            if (currentDate < course.StartDate) Status = "Upcoming";

            if (currentDate >= course.StartDate && currentDate <= course.EndDate) Status = "Ongoing";

            if (currentDate > course.EndDate) Status = "Completed";

            Students = course.Students.Select(s => new StudentGetViewModel(s)).ToList();
        }
    }
}