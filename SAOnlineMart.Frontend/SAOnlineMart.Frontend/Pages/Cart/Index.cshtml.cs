using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAOnlineMart.Frontend.Models; // Ensure this namespace matches your models
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAOnlineMart.Frontend.Pages.Cart
{
    public class IndexModel : PageModel
    {

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void OnGet()
        {

            CartItems = GetCartItems();
        }

        private List<CartItem> GetCartItems()
        {
            return new List<CartItem>
            {
                new CartItem { Product = new Product { Id = 1, Name = "Sample Product", Price = 10.00M }, Quantity = 2 },
                new CartItem { Product = new Product { Id = 2, Name = "Another Product", Price = 20.00M }, Quantity = 1 }
            };
        }

        public IActionResult OnPostCheckout()
        {

            CartItems.Clear();

            return RedirectToPage("/Order/Confirmation");
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
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int Stock { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
