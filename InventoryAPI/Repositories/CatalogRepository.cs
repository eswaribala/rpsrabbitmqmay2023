using InventoryAPI.Contexts;
using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly InventoryContext _dbContext;

        public CatalogRepository(InventoryContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Catalog AddCatalog(Catalog catalog)
        {
            var result = _dbContext.Catalogs.Add(catalog);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteCatalog(long Id)
        {
            var filteredData = _dbContext.Catalogs.Where(x => x.CatalogId == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }

        public Catalog GetCatalogById(long id)
        {

            return _dbContext.Catalogs.Where(x => x.CatalogId == id).FirstOrDefault();
        }

        public IEnumerable<Catalog> GetCatalogList()
        {
            return _dbContext.Catalogs.ToList();
        }

        public Catalog UpdateCatalog(Catalog catalog)
        {
            var result = _dbContext.Catalogs.Update(catalog);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
