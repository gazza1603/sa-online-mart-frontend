using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SAOnlineMartAPI");
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _httpClient.GetFromJsonAsync<Product>($"products/{id}");

            if (Product == null)
            {
                return NotFound();
            }

            // Debugging output
            Console.WriteLine($"Product data - ID: {Product.Id}, Name: {Product.Name}, Price: {Product.Price}, Stock: {Product.Stock}, ImageUrl: {Product.ImageUrl}");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Send the PUT request to the API to update the product
                var response = await _httpClient.PutAsJsonAsync($"http://localhost:5059/api/products/{Product.Id}", Product);

                // If the response indicates success, redirect to the product index page
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Admin/Products/Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Failed to update product: {response.StatusCode}. {errorContent}");

                    Console.Error.WriteLine($"Error updating product: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details and return an error message
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                Console.Error.WriteLine($"Exception during product update: {ex}");
            }
            return Page();
        }

    }
}
