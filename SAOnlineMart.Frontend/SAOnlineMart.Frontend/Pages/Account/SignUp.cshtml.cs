using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Account
{
    public class SignUpModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public User NewUser { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public SignUpModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SAOnlineMartAPI");
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5059/api/users", NewUser);

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to the login page after successful sign-up
                    return RedirectToPage("/Account/Login");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync(); 
                    ErrorMessage = $"Sign-up failed: {errorContent}";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

    }
}
