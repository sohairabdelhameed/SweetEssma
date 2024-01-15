using Microsoft.AspNetCore.Mvc;
using SweetEssma.Models;
using SweetEssma.ViewModels;

namespace SweetEssma.Components
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly IShoppingCart _shoppingCart;
        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shopingCartItems= items;
            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart,_shoppingCart.GetShoppingCartTotal());
            return View(shoppingCartViewModel);
        }

    }
}
