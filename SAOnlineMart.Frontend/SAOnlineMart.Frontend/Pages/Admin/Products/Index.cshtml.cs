using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SAOnlineMart.Frontend.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient("SAOnlineMartAPI");
            _logger = logger;
        }

        public List<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete product with ID: {ProductId}", id);

            var response = await _httpClient.DeleteAsync($"products/{id}");

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully deleted product with ID: {ProductId}", id);
                return RedirectToPage();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to delete product with ID: {ProductId}. Status Code: {StatusCode}. Error: {ErrorContent}",
                             id, response.StatusCode, errorContent);

            ModelState.AddModelError(string.Empty, $"Failed to delete product with ID: {id}. Error: {errorContent}");

            return Page();
        }
    }
}
