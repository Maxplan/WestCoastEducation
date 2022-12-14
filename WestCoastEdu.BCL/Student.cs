using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class Student : Account
    {
        public List<Course> ActiveCourses = new List<Course>(); // list of active courses
        public List<Course> CompletedCourses = new List<Course>(); // List of completed courses
        public List<Course> SignedUpCourses = new List<Course>(); // List of courses a student has signed up for

        public Student(string firstName, string lastName, string socialSecurityNumber){
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            AccountRole = nameof(Student);
            }
        public void ListActiveCourses(){
            System.Console.WriteLine("--Accessed Courses--");
            for (int i = 0; i < ActiveCourses.Count; i++)
            {
                System.Console.WriteLine(ActiveCourses[i].CourseName);
            }
        }
        public void ListCompletedCourses(){
            System.Console.WriteLine("--Completed Courses--");
            for (int i = 0; i < CompletedCourses.Count; i++)
            {
                System.Console.WriteLine(CompletedCourses[i].CourseName);
            }
        }
        public void EnrollCourse(Course course){
            ActiveCourses.Add(course);
            SignedUpCourses.Add(course);
        }
    }
}
