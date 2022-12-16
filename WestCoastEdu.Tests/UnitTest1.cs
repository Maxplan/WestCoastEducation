using WestCoastEdu.BCL;

namespace WestCoastEdu.Tests;

public class StudentTest
{
    [Fact]
    public void StudentHasSocialSecurityNumber_UponCreation()
    {
        var s = new Student("FirstName", "LastName", "SocialSecurityNumber");
        bool result = true;

        if (string.IsNullOrEmpty(s.SocialSecurityNumber) || string.IsNullOrWhiteSpace(s.SocialSecurityNumber))
        {
            result = false;
        }

        Assert.True(result);
    }
    
    [Fact]
    public void StudentHasFirstName_UponCreation()
    {
        var s = new Student("FirstName", "LastName", "SocialSecurityNumber");
        bool result = true;

        if (string.IsNullOrEmpty(s.FirstName) || string.IsNullOrWhiteSpace(s.FirstName))
        {
            result = false;
        }

        Assert.True(result);
    }

    [Fact]
    public void StudentHasLastName_UponCreation()
    {
        var s = new Student("FirstName", "LastName", "SocialSecurityNumber");
        bool result = true;

        if (string.IsNullOrEmpty(s.LastName) || string.IsNullOrWhiteSpace(s.LastName))
        {
            result = false;
        }

        Assert.True(result);
    }

    [Fact]
    public void Handler_AddStudentToCourse_Student_ActiveCourses_ShouldBe_1()
    {
        var handler = new Handler();
        var student = new Student("123", "123", "123");
        var kurs = new Course("Svenska", false, true, new DateTime(2022, 12, 14), new DateTime(2023, 01, 01));

        handler.AddStudentToCourse(student, kurs);

        Assert.Equal(1, student.ActiveCourses.Count);
    }
    [Fact]
    public void Handler_AddStudentToCourse_Student_SignedUpCourses_ShouldBe_1()
    {
        var handler = new Handler();
        var student = new Student("123", "123", "123");
        var kurs = new Course("Svenska", false, true, new DateTime(2022, 12, 14), new DateTime(2023, 01, 01));

        handler.AddStudentToCourse(student, kurs);

        Assert.Equal(1, student.SignedUpCourses.Count);
    }
    [Fact]
    public void Handler_AddStudentToCourse_Course_SignedUpStudents_ShouldBe_1()
    {
        var handler = new Handler();
        var student = new Student("123", "123", "123");
        var kurs = new Course("Svenska", false, true, new DateTime(2022, 12, 14), new DateTime(2023, 01, 01));

        handler.AddStudentToCourse(student, kurs);

        Assert.Equal(1,kurs.SignedUpStudents.Count);
    }
    [Fact]
    public void Handler_AddCourseToStudent_Student_ActiveCourses_ShouldBe_1()
    {
        var handler = new Handler();
        var student = new Student("123", "123", "123");
        var kurs = new Course("Svenska", false, true, new DateTime(2022, 12, 14), new DateTime(2023, 01, 01));

        handler.AddCourseToStudent(student, kurs);

        Assert.Equal(1, student.ActiveCourses.Count);
    }
    [Fact]
    public void Handler_AddCourseToStudent_Student_SignedUpCourses_ShouldBe_1()
    {
        var handler = new Handler();
        var student = new Student("123", "123", "123");
        var kurs = new Course("Svenska", false, true, new DateTime(2022, 12, 14), new DateTime(2023, 01, 01));

        handler.AddCourseToStudent(student, kurs);

        Assert.Equal(1, student.SignedUpCourses.Count);
    }
}