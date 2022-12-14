using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoastEdu.BCL;


namespace WestCoastEdu.BCL;
class Program
{
    static void Main(string[] args)
    {   
        Student student = new Student("123", "123", "123");
        var kurs = new Course("Svenska", false, true, new DateTime(2022, 12, 14), new DateTime(2023, 01, 01));

        System.Console.WriteLine(kurs.CourseName);

        var ny = new Handler();
        ny.AddStudentToCourse(student, kurs);

        System.Console.WriteLine(student.ActiveCourses[0].CourseName);
    }
}
