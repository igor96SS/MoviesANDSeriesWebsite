using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies_SeriesWebsite.Models;
using System.Text;
using System.Text.Json;

namespace Movies_SeriesWebsite.Pages.Series
{
    public class EditSerieModel : PageModel
    {

        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public EditSerieModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public SeriesModel SeriesModel { get; set; }
        public async Task OnGet(int id)
        {
            // Create the HTTP client using the FruitAPI named factory
            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            // Execute the GET operation and store the response
            using HttpResponseMessage response = await httpClient.GetAsync("series/" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                SeriesModel = await JsonSerializer.DeserializeAsync<SeriesModel>(contentStream);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(SeriesModel), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Movies&SeriesAPI");

            using HttpResponseMessage response = await httpClient.PutAsync("series/" + SeriesModel.serieID.ToString(), jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was edited successfully.";
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
