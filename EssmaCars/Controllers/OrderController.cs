using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweetEssma.Models;

namespace SweetEssma.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;
        public OrderController(IOrderRepository orderRepository,IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;


        }
        public IActionResult Checkout() //Get
        {
            return View();
        }
        [HttpPost]
         public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shopingCartItems = items;
            if(_shoppingCart.shopingCartItems.Count == 0) 
            {
                ModelState.AddModelError("", "Your Cart is empty, add some pies first");
            
            }
            if(ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");

            }
            return View(order);

        }
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for your Order . You will enjoy our made with love pies as soon as possible";
            return View();
        }
    }
}
