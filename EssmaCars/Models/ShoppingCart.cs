using EssmaCars.Models;
using Microsoft.EntityFrameworkCore;

namespace SweetEssma.Models
{

    public class ShoppingCart : IShoppingCart
    {
        //create db contesxt
        private readonly SweetEssmaDbContext _sweetEssmaDbContext;
        public string? ShoppingCartId { get; set; }

        public List<ShopingCartItem> shopingCartItems { get; set; } = default!;


        //inject dbcontext created in the constructor
        private ShoppingCart(SweetEssmaDbContext sweetEssmaDbContext)
        {
            _sweetEssmaDbContext = sweetEssmaDbContext;
        }
        //not in the interface becuase it is a static method
        //it will return a fully created shopping cart
        //the service collection passed : when the user visits the site the code will run to check if
        //already an ID called CartId For that user
        // if not it wull create a new GUID and restore that id
        //we are storing between different requests information about the users
        //from that we are using sessions 
        //Sessions will give me the ability to store information about a returning users
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            SweetEssmaDbContext context = services.GetService<SweetEssmaDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem =
                    _sweetEssmaDbContext.ShopingCartItems.SingleOrDefault(
                        s => s.pie.PieId == pie.PieId && s.ShopingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShopingCartItem
                {
                    ShopingCartId = ShoppingCartId,
                    pie = pie,
                    Amount = 1
                };

                _sweetEssmaDbContext.ShopingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _sweetEssmaDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _sweetEssmaDbContext.ShopingCartItems.SingleOrDefault(
                        s => s.pie.PieId == pie.PieId && s.ShopingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _sweetEssmaDbContext.ShopingCartItems.Remove(shoppingCartItem);
                }
            }

            _sweetEssmaDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShopingCartItem> GetShoppingCartItems()
        {
            return shopingCartItems ??=
                       _sweetEssmaDbContext.ShopingCartItems.Where(c => c.ShopingCartId == ShoppingCartId)
                           .Include(s => s.pie)
                           .ToList();
        }


        public void ClearCart()
        {
            var cartItems = _sweetEssmaDbContext
                .ShopingCartItems
                .Where(cart => cart.ShopingCartId == ShoppingCartId);

            _sweetEssmaDbContext.ShopingCartItems.RemoveRange(cartItems);

            _sweetEssmaDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _sweetEssmaDbContext.ShopingCartItems.Where(c => c.ShopingCartId == ShoppingCartId)
                .Select(c => c.pie.Price * c.Amount).Sum();
            return total;
        }
    }
     
}
