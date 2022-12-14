using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class Teacher : Account
    {
        public List<Course> CoursesTaught { get; set; } = new List<Course>();
        // Denna constructor sätter även ett guid eftersom det är specat i properties.
        // Båda kraven för nytt konto är då uppfyllda.
        public Teacher(string ssn)
        {
            SocialSecurityNumber = ssn;
        }
   
        public void ListCoursesTaught(){
            System.Console.WriteLine("--Courses Taught--");
            for (int i = 0; i < CoursesTaught.Count; i++)
            {
                System.Console.WriteLine(CoursesTaught[i].CourseName);
            }
        }
        public void MessageAllStudent(string message){}

        public void MessageStudent(string studentId, string message){}
    }
}