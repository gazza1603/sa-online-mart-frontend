using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models;
using System.Net.Http;
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
            // Fetch the product to confirm deletion
            Product = await _httpClient.GetFromJsonAsync<Product>($"products/{id}");

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Console.WriteLine("OnPostAsync method started.");

            // Delete the product
            var response = await _httpClient.DeleteAsync($"products/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Products/Index");
            }

            // Handle errors
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"An error occurred while deleting the product: {errorContent}");

            return Page();
        }
    }
}
