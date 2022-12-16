using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class Handler
    {
        public void AddStudentToCourse(Student student, Course course){
            course.AddNewStudent(student);
            student.EnrollCourse(course);
        }
        public void AddCourseToStudent(Student student, Course course){
            course.AddNewStudent(student);
            student.EnrollCourse(course);
        }
    }
}