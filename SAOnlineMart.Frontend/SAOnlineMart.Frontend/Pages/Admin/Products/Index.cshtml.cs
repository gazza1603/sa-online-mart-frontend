using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace SAOnlineMart.Frontend.Pages.Admin.Products
{
	public class IndexModel : PageModel
	{
		private readonly HttpClient _httpClient;

		public IndexModel(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("SAOnlineMartAPI");
		}

		public List<Product> Products { get; set; }

		public async Task OnGetAsync()
		{
			Products = await _httpClient.GetFromJsonAsync<List<Product>>("products");
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			await _httpClient.DeleteAsync($"products/{id}");
			return RedirectToPage();
		}
	}
}
