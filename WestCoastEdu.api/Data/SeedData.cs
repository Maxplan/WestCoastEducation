using System.Text.Json;
using WestCoastEdu.api.Models;

namespace WestCoastEdu.api.Data
{
    public class SeedData
    {
        public static async Task LoadCourseData(WestCoastEduContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Courses.Any()) return;

            var json = System.IO.File.ReadAllText("Data/json/CourseTestData.json");

            var courses = JsonSerializer.Deserialize<List<Course>>(json, options);

            if (courses is not null && courses.Count > 0)
            {
                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadStudentData(WestCoastEduContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Students.Any()) return;

            var json = System.IO.File.ReadAllText("Data/json/StudentsTestData.json");

            var students = JsonSerializer.Deserialize<List<Student>>(json, options);

            if (students is not null && students.Count > 0)
            {
                await context.Students.AddRangeAsync(students);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadTeacherData(WestCoastEduContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Teachers.Any()) return;

            var json = System.IO.File.ReadAllText("Data/json/TeachersTestData.json");

            var teachers = JsonSerializer.Deserialize<List<Teacher>>(json, options);

            if (teachers is not null && teachers.Count > 0)
            {
                await context.Teachers.AddRangeAsync(teachers);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadCompetenceData(WestCoastEduContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Competences.Any()) return;

            var json = System.IO.File.ReadAllText("Data/json/CompetencesTestData.json");

            var competences = JsonSerializer.Deserialize<List<Competence>>(json, options);

            if (competences is not null && competences.Count > 0)
            {
                await context.Competences.AddRangeAsync(competences);
                await context.SaveChangesAsync();
            }
        }
    }
}