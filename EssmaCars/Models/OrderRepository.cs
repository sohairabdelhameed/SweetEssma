using EssmaCars.Models;

namespace SweetEssma.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SweetEssmaDbContext _sweetEssmaDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(SweetEssmaDbContext sweetEssmaDbContext, IShoppingCart shoppingCart)
        {
            _sweetEssmaDbContext = sweetEssmaDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShopingCartItem>? shoppingCartItems = _shoppingCart.shopingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            //adding the order with its details

            foreach (ShopingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.pie.PieId,
                    Price = shoppingCartItem.pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _sweetEssmaDbContext.orders.Add(order); 

            _sweetEssmaDbContext.SaveChanges();
        }
    }
}
