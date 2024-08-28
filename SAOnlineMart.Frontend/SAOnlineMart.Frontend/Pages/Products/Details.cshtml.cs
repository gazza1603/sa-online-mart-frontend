using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models;  // Ensure this is the correct namespace for your Product model
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SAOnlineMartAPI");
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _httpClient.GetFromJsonAsync<Product>($"products/{id}");

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
