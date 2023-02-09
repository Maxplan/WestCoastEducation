using System.ComponentModel.DataAnnotations.Schema;

namespace WestCoastEdu.api.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int CourseNumber { get; set; }
        public int DurationWk { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CourseStatusEnum Status { get; set;} = CourseStatusEnum.Upcoming;
        public int? TeacherId { get; set; }
        public bool IsFull { get; set; } = false;
        public ICollection<Student> Students { get; set; } = new List<Student>();
        // Navigation Properties
        [ForeignKey("TeacherId")] // Vilken property ska den främmande nyckeln referera till
        public Teacher Teacher { get; set; }
    }
}