namespace WestCoastEdu.web.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TeacherListViewModel Teacher { get; set;}
        public ICollection<StudentListViewModel> Students { get; set; }
    }
}