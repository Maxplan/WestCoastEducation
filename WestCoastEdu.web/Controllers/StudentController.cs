using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.web.ViewModels;

namespace WestCoastEdu.web.Controllers
{
    [Route("students")]
    public class StudentController : Controller
    {
        public readonly IConfiguration _config;
        public readonly IHttpClientFactory _httpClient;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;

        public StudentController(IConfiguration config, IHttpClientFactory httpClient)
        {
            _config = config;
            _httpClient = httpClient;
            _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true }; 
        }
        public async Task<IActionResult> Index()
        {
            using var client = _httpClient.CreateClient();
            // Get Data from API
            var response = await client.GetAsync($"{_baseUrl}/students/listall");

            if(!response.IsSuccessStatusCode) return StatusCode(500, "Something went wrong whn trying to fetch data from API");

            var json = await response.Content.ReadAsStringAsync();

            var students = JsonSerializer.Deserialize<IList<StudentListViewModel>>(json, _options);

            return View("Index", students);
        }

        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            // instance of http client
            using var client = _httpClient.CreateClient();
            // Get Data from API
            var response = await client.GetAsync($"{_baseUrl}/students/getbyid/{id}");

            if(!response.IsSuccessStatusCode) return StatusCode(500, "Something went wrong when trying to fetch data from API");

            var json = await response.Content.ReadAsStringAsync();

            var student = JsonSerializer.Deserialize<StudentDetailsViewModel>(json, _options);

            return View("Details", student);
        }
    }
}