using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies_SeriesWebsite.Models;
using System.Text;
using System.Text.Json;

namespace Movies_SeriesWebsite.Pages.Movies
{
    public class EditMovieModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public EditMovieModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public MoviesModel MoviesModel { get; set; }
        public async Task OnGet(int id)
        {

            // Create the HTTP client using the FruitAPI named factory
            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            // Execute the GET operation and store the response
            using HttpResponseMessage response = await httpClient.GetAsync("movies/" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                MoviesModel = await JsonSerializer.DeserializeAsync<MoviesModel>(contentStream);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(MoviesModel), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            using HttpResponseMessage response = await httpClient.PutAsync("movies/" + MoviesModel.id.ToString(), jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was edited successfully.";
                return RedirectToPage("MoviesList");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("MoviesList");
            }
        }
    }
}
