using Ciceksepeti.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ciceksepeti.DataAccess
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
