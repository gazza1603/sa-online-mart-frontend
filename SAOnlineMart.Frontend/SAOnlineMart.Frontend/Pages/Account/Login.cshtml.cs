using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using SAOnlineMart.Frontend.Models; // Ensure correct namespace

namespace SAOnlineMart.Frontend.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync method started.");
            Console.WriteLine($"Username: {Username}, Password: {Password}");

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and Password are required.";
                Console.WriteLine("Model validation failed.");
                return Page();
            }

            // Simulate login logic here

            return RedirectToPage("/Index");
        }
    }

}
