using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies_SeriesWebsite.Models;
using System.Text;
using System.Text.Json;

namespace Movies_SeriesWebsite.Pages.Series
{
    public class SeriesListModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public SeriesListModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public IEnumerable<SeriesModel> SeriesModelGET { get; set; }

        public async Task OnGet()
        {
            // Create the HTTP client using the FruitAPI named factory
            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            // Execute the GET operation and store the response
            using HttpResponseMessage response = await httpClient.GetAsync("series");

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                SeriesModelGET = await JsonSerializer.DeserializeAsync<IEnumerable<SeriesModel>>(contentStream);
            }
        }

        [BindProperty]
        public SeriesModel SeriesModelPOST { get; set; }

        public async Task<IActionResult> OnPost()
        {

            var jsonContent = new StringContent(JsonSerializer.Serialize(SeriesModelPOST), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            using HttpResponseMessage response = await httpClient.PostAsync("series", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was added successfully.";
                return RedirectToPage("SeriesList");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("SeriesList");
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            using HttpResponseMessage response = await httpClient.DeleteAsync("series/" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was deleted successfully.";
                return RedirectToPage("SeriesList");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("SeriesList");
            }
        }
    }
}
