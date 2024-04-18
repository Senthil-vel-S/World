using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorldWebAPI.DTO;

namespace WorldWebAPI.Pages
{
    public class CountryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CountryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public List<CountryDto> Countries { get; set; }
        public async void OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("WorldWebAPI");
            Countries = await httpClient.GetFromJsonAsync<List<CountryDto>>("api/Country");
        }
    }
}
