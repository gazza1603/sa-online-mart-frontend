using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SAOnlineMartAPI");
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync method started.");

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return Page();
            }

            // Post the product data to the correct API endpoint
            var response = await _httpClient.PostAsJsonAsync("products", Product);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Products/Index");
            }

            // Handle errors: capture error content from the response
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"An error occurred while creating the product: {errorContent}");

            return Page();
        }

    }
}
