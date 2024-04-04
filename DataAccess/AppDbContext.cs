using Microsoft.EntityFrameworkCore;
using productApi.Model;

namespace productApi.DataAccess
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
                
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }
    }
}
