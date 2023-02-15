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
        public CourseStatusEnum Status { get; set; } = CourseStatusEnum.Upcoming; // Försök styra denna via start och end date
        public int? TeacherId { get; set; }
        public bool IsFull { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
    }
}