using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class Customer : IAccount
    {
        public List<Course> ActiveCourses = new List<Course>(); // list of bought and active Courses
        public List<Course> CompletedCourses = new List<Course>(); // List of completed courses
        public List<Course> OnDemandCourses = new List<Course>(); // List of purchased courses
        public List<Course> SignedUpCourses = new List<Course>(); // List of courses a student has signed up for

        public Customer(string firstName, string lastName, string socialSecurityNumber){
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            AccountRole = nameof(Customer);
            }
        public void ListActiveCourses(){ // List the name of all active courses a customer attends
            System.Console.WriteLine("--Active Courses--");
            for (int i = 0; i < ActiveCourses.Count; i++)
            {
                System.Console.WriteLine(ActiveCourses[i].CourseName);
            }
        }
        public void ListCompletedCourses(){ // List the name of all completed courses by customer
            System.Console.WriteLine("--Completed Courses--");
            for (int i = 0; i < CompletedCourses.Count; i++)
            {
                System.Console.WriteLine(CompletedCourses[i].CourseName);
            }
        }
        public void ListOnDemandCourses(){ // List the name of all On Demand courses bought by customer.
            System.Console.WriteLine("--On Demand Courses--"); // List the name of all courses purchased by a customer
            for (int i = 0; i < OnDemandCourses.Count; i++)
            {
                System.Console.WriteLine(OnDemandCourses[i].CourseName);
            }
        }
        public void EnrollCourse(Course course){ // List all courses a customer has signed up for
            ActiveCourses.Add(course);
            SignedUpCourses.Add(course);
        }
    }
}