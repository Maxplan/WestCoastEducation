namespace WestCoastEdu.web.ViewModels
{
    public class CourseListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseNumber { get; set; }
        public int Teacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}