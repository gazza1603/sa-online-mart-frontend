using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> CartItems { get; set; }

        public async Task OnGetAsync()
        {
            CartItems = GetCartItems();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            RemoveFromCart(id);
            return RedirectToPage();
        }

        private List<CartItem> GetCartItems()
        {
            return new List<CartItem>();
        }

        private void RemoveFromCart(int id)
        {
        }

        public class CartItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
