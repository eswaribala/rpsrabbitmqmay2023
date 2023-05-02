using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Contexts
{
    public class InventoryContext:DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> 
            options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Catalog> Catalogs
        {
            get;
            set;
        }
        public DbSet<Product> Products
        {
            get;
            set;
        }
    }
}
