using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies_SeriesWebsite.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Movies_SeriesWebsite.Pages.Movies
{
    public class MoviesListModel : PageModel
    {

        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public MoviesListModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        // Adds the data model and binds the form data to the model properties
        // Enumerable since an array is expected as a response
        [BindProperty]
        public IEnumerable<MoviesModel> MoviesModelGET { get; set; }

        public async Task OnGet()
        {
            // Create the HTTP client using the FruitAPI named factory
            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            // Execute the GET operation and store the response
            using HttpResponseMessage response = await httpClient.GetAsync("movies");

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                MoviesModelGET = await JsonSerializer.DeserializeAsync<IEnumerable<MoviesModel>>(contentStream);
            }

        }

        [BindProperty]
        public MoviesModel MoviesModelPOST { get; set; }

        public async Task<IActionResult> OnPost()
        {

            var jsonContent = new StringContent(JsonSerializer.Serialize(MoviesModelPOST), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            using HttpResponseMessage response = await httpClient.PostAsync("movies", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was added successfully.";
                return RedirectToPage("MoviesList");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("MoviesList");
            }
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            using HttpResponseMessage response = await httpClient.DeleteAsync("movies/" + MoviesModelPOST.id.ToString());

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was deleted successfully.";
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
