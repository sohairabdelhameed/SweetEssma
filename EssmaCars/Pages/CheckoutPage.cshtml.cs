using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetEssma.Models;

namespace SweetEssma.Pages
{
    public class CheckoutPageModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;
        public CheckoutPageModel(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
         
        }

        public Order Order { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shopingCartItems = items;

            if (_shoppingCart.shopingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your Cart is empty,add some pies");

            }
            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(Order);
                _shoppingCart.ClearCart();
                return RedirectToPage("CheckOutCompletePage");

            }
            return Page();
        }
    }
}
