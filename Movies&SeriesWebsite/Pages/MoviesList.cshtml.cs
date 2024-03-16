using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies_SeriesWebsite.Models;
using System.Text.Json;

namespace Movies_SeriesWebsite.Pages
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
        public IEnumerable<MoviesModel> MoviesList { get; set; }

        public async Task OnGet()
        {
            // Create the HTTP client using the FruitAPI named factory
            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            // Execute the GET operation and store the response
            using HttpResponseMessage response = await httpClient.GetAsync("movies");

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                MoviesList = await JsonSerializer.DeserializeAsync<IEnumerable<MoviesModel>>(contentStream);
            }

        }


    }
}
