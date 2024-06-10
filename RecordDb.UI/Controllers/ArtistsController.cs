using Ganss.Xss;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //// Testing
            // ViewBag.Id = id;

            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<ArtistDto>($"https://localhost:7262/api/artists/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtistDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7262/api/artists/{request.ArtistId}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ArtistDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Artists");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ArtistDto>($"https://localhost:7262/api/artists/{id.ToString()}");

            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Add("img");
            sanitizer.AllowedAttributes.Add("src");
            sanitizer.AllowedAttributes.Add("alt");
            sanitizer.AllowedAttributes.Add("title");

            if (response is not null)
            {
                if (response.Biography is not null)
                {
                    response.Biography = sanitizer.Sanitize(response.Biography);
                }

                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ArtistDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7262/api/artists/{request.ArtistId}");

                var response = httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Artists");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
