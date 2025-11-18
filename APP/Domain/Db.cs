using Microsoft.EntityFrameworkCore;

namespace APP.Domain
{
    public class Db : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductStore> ProductStores { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
        }

        // Extra: Database configuration
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
