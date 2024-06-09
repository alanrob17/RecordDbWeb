using Microsoft.AspNetCore.Mvc;
using RecordDb.UI.Models;
using RecordDb.UI.Models.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RecordDb.UI.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ArtistsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ArtistDto> response = new List<ArtistDto>();

            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7262/api/artists");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ArtistDto>>());
            }
            catch (Exception ex)
            {

                // log the exception
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddArtistViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage() 
            { 
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7262/api/artists"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ArtistDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Artists");
            }

            return View(response);
        }
    }
}
