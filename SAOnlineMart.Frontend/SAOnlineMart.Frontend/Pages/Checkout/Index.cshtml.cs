using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        public CustomerModel Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Process the order
            return RedirectToPage("/Order/Confirmation", new { orderId = 123 });
        }

        public class CustomerModel
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string PaymentMethod { get; set; }
        }
    }
}
