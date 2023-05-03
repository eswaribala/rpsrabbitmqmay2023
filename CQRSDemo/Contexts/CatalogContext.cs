using CQRSDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSCartAPI.Contexts
{
    public class CatalogContext:DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }
       
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
