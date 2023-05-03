using CQRSCartAPI.Contexts;
using CQRSDemo.Models;

namespace CQRSDemo.Repositories
{
    public class CatalogSqliteRepository
    {
        private readonly CatalogContext _catalogContext;

        public CatalogSqliteRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext; 
        }
        public Catalog Create(Catalog catalog)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Catalog> entry =
                _catalogContext.Catalogs.Add(catalog);
            _catalogContext.SaveChanges();
            return entry.Entity;
        }
        public void Update(Catalog Catalog)
        {
            _catalogContext.Catalogs.Update(Catalog);
            _catalogContext.SaveChanges();
        }
        public void Remove(long id)
        {
            _catalogContext.Catalogs.Remove(GetById(id));
            _catalogContext.SaveChanges();
        }
        public IQueryable<Catalog> GetAll()
        {
            return _catalogContext.Catalogs;
        }
        public Catalog GetById(long id)
        {
            return _catalogContext.Catalogs.Find(id);
        }

    }
}
