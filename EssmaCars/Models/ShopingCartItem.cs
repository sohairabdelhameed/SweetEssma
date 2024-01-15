using EssmaCars.Models;

namespace SweetEssma.Models
{
    public class ShopingCartItem
    {
        public int ShopingCartItemId { get; set; }
        public Pie pie { get; set; } = default!;
        public int Amount { get; set; }
        public string? ShopingCartId { get; set; }  
    }
}
