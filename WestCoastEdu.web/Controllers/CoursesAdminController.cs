using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.web.ViewModels;

namespace WestCoastEdu.web.Controllers;

[Route("courses/admin")]
public class CoursesAdminController : Controller
{
    public readonly IConfiguration _config;
    public readonly IHttpClientFactory _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;

    public CoursesAdminController(IConfiguration config, IHttpClientFactory httpClient)
    {
        _config = config;
        _httpClient = httpClient;
        _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
        _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true }; 
    }
    public async Task<IActionResult> Index()
    {
        // instance of http client
        using var client = _httpClient.CreateClient();
        // Get Data from API
        var response = await client.GetAsync($"{_baseUrl}/courses/listall");

        if(!response.IsSuccessStatusCode) return StatusCode(500, "Something went wrong whn trying to fetch data from API");

        var json = await response.Content.ReadAsStringAsync();

        var courses = JsonSerializer.Deserialize<IList<CourseListViewModel>>(json, _options);

        return View("Index", courses);
    }
    [Route("create")]
    public IActionResult Create()
    {
        var model = new CoursePostViewModel();
        return View(model);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CoursePostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var coursePost = new CoursePostViewModel
        {
            Title = model.Title,
            CourseNumber = model.CourseNumber,
            DurationWk = model.DurationWk,
            StartDate = model.StartDate,
        };

        using var client = _httpClient.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize(coursePost), Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{_baseUrl}/courses/add", content);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "An error occurred while trying to create the course.";
            return View(model);
        }

        return RedirectToAction("Index");
    }
}
