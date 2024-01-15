using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using SweetEssma.Models;

namespace EssmaCars.Models
{
    public class SweetEssmaDbContext: IdentityDbContext 
    {
        public SweetEssmaDbContext(DbContextOptions<SweetEssmaDbContext> options)
            :base(options)
        {

        }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Pie> Pies { get; set; }
        public DbSet<ShopingCartItem> ShopingCartItems{ get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
    }
}
