using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public interface ICatalogRepository
    {
        public IEnumerable<Catalog> GetCatalogList();
        public Catalog GetCatalogById(long id);
        public Catalog AddCatalog(Catalog catalog);
        public Catalog UpdateCatalog(Catalog catalog);
        public bool DeleteCatalog(long Id);
    }
}
