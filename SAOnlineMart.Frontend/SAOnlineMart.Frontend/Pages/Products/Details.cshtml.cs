using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models;
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
            Product = await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
            if (product == null)
            {
                return NotFound();
            }

            Console.WriteLine($"Product {product.Name} added to cart.");

            return RedirectToPage("/Cart/Index");
        }
    }
}
