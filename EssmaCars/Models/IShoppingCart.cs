using EssmaCars.Models;

namespace SweetEssma.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Pie pie);
        int RemoveFromCart(Pie pie);    
        List<ShopingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShopingCartItem> shopingCartItems { set; get; }
    }
}
