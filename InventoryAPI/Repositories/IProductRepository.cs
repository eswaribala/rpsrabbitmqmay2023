using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetProductList();
        public Product GetProductById(long id);
        public Product AddProduct(Product product);
        public Product UpdateProduct(Product product);
        public bool DeleteProduct(long Id);
    }
}
