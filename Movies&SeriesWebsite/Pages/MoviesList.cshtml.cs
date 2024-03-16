using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies_SeriesWebsite.Models;
using System.Text.Json;

namespace Movies_SeriesWebsite.Pages
{
    public class MoviesListModel : PageModel
    {

        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;

        public MoviesListModel(HttpClient httpClient, IConfiguration iConfiguration)
        {
            _httpClient = httpClient;
            _configuration = iConfiguration;
        }
        

        public async Task OnGetAsync()
        {

            string baseUrl = _configuration.GetSection("ApiSettings").GetSection("BaseUrl").Value;

            var response = await _httpClient.GetAsync(baseUrl + "movies");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<List<Movies>>(content);
            ViewData["Movies"] = movies;
        }


    }
}
