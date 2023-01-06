using System.Text.Json;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Data;

public static class SeedData
{
    public static async Task LoadCourseData(WestCoastEduContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Vill endast ladda data om databasens tabell är tom...
        if (context.Courses.Any()) return;

        // Läsa in json datat...
        var json = System.IO.File.ReadAllText("Data/json/courses.json");
        // Konvertera json objekten till en lista av Course objekt...
        var courses = JsonSerializer.Deserialize<List<Course>>(json, options);

        if (courses is not null && courses.Count > 0)
        {
            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadAccountData(WestCoastEduContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Vill endast ladda data om databasens tabell är tom...
        if (context.Accounts.Any()) return;

        // Läsa in json datat...
        var json = System.IO.File.ReadAllText("Data/json/accounts.json");
        // Konvertera json objekten till en lista av Course objekt...
        var accounts = JsonSerializer.Deserialize<List<Account>>(json, options);

        if (accounts is not null && accounts.Count > 0)
        {
            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
        }
    }
}
