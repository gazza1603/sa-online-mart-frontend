using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SAOnlineMartAPI");
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Console.WriteLine("OnGetAsync method started for product ID: " + id);
            // Fetch the product details to display before deletion
            Product = await _httpClient.GetFromJsonAsync<Product>($"products/{id}");

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Console.WriteLine("OnPostAsync method started for product ID: " + id);
            var response = await _httpClient.DeleteAsync($"products/{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Product deleted successfully.");
                return RedirectToPage("/Admin/Products/Index");
            }
            else
            {
                Console.WriteLine("Failed to delete product.");
                return Page();
            }
        }
    }
}
