using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoastEdu.BCL
{
    public class Course
    {
        public int CourseId { get; init; }
        public string? CourseName { get; private set; }
        public bool OnDemand { get; private set; } // Can be purchased?
        public bool IsDistance {get; private set;} // Distance or classroom bound?
        public DateTime StartDate { get; private set; } = new DateTime();
        public DateTime EndDate { get; private set; } = new DateTime();
        public List<IAccount> SignedUpStudents { get; private set; } = new List<IAccount>(); // List all students that has signed up for the course
        public List<Teacher> Teachers { get; private set; } = new List<Teacher>(); // List of teacher/-s of the course
        public string? CourseDescription { get; set; }

        public Course(string courseName, bool onDemand, bool isDistance, DateTime startDate, DateTime endDate) // Constructor as all parameters are need to create a complete course object
        {
            CourseName = courseName;
            OnDemand = onDemand;
            IsDistance = isDistance;
            StartDate = startDate;
            EndDate = endDate;
        }
        public void CourseInfo(){ // List information about the course
            System.Console.WriteLine($"--{CourseName}--");
            if (OnDemand){
                System.Console.WriteLine("On Demand");
            }else{
                System.Console.WriteLine("Not On Demand");
            }
            if (IsDistance){
                System.Console.WriteLine("Distance Course");
            }else{
                System.Console.WriteLine("Classroom Bound");
            }

            System.Console.WriteLine("Start Date: " + StartDate);
            System.Console.WriteLine("End Date: " + EndDate);
            System.Console.WriteLine("--Teachers--"); // Lists the teachers of the course
            for (int i = 0; i < Teachers.Count; i++)
            {
                System.Console.WriteLine(Teachers[i].SocialSecurityNumber + " " + Teachers[i].LastName +"\n");
            }
        }
        public void NewCourseName(string courseName) => CourseName = courseName; // To change name in case an error occured in the constructor
        public void NewOnDemand(bool onDemand) => OnDemand = onDemand; // To change on demand status in case an error occured in the constructor
        public void NewStartDate(DateTime startDate) => StartDate = startDate; // To change start date in case an error occured in the constructor
            
        public void NewEndDate(DateTime endDate) => EndDate = endDate;
        public void AddNewStudent(Student student){ // adds a instance of student to the list SignedUpStudents, directly followed by pushing the instance of course that called the methods, into the ActiveCourses list on an instance of student
            SignedUpStudents.Add(student); 
        }
        public void AddNewCustomer(Customer customer) => SignedUpStudents.Add(customer);
        public void AddNewTeacher(Teacher teacher) => Teachers.Add(teacher);
        public void AllSignedUpStudents(){ // List the ssn of all signed up students
            for (int i = 0; i < SignedUpStudents.Count; i++)
            {
                System.Console.WriteLine(SignedUpStudents[i].SocialSecurityNumber);
            }
            System.Console.WriteLine("Total: " + SignedUpStudents.Count);
        }

    }
}