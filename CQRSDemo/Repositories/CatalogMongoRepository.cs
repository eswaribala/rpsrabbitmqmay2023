using CQRSDemo.Models;
using MongoDB.Driver;

namespace CQRSDemo.Repositories
{
    public class CatalogMongoRepository
    {
        private IConfiguration _configuration;
        private IMongoDatabase _db;
        private string DBName;
        private string CollectionName;
        private string Url;
        public CatalogMongoRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            DBName = _configuration["MongoDbName"];
            CollectionName = _configuration["MongoCollectionName"];
            Url = _configuration["Url"];
            MongoClient _client = new MongoClient(Url);
            _db = _client.GetDatabase(DBName);
        }
       
        public List<CatalogEntity> GetCarts()
        {
            return _db.GetCollection<CatalogEntity>(CollectionName).Find(_ => true).ToList();
        }
        public CatalogEntity GetCart(long id)
        {
            return _db.GetCollection<CatalogEntity>(CollectionName).Find
                (catalog => catalog.CatalogId == id).SingleOrDefault();
        }
        public CatalogEntity GetCartByCartName(string CatalogName)
        {
            return _db.GetCollection<CatalogEntity>(CollectionName).Find
                (catalog => catalog.CatalogName == CatalogName).Single();
        }
        public void Create(CatalogEntity CatalogEntity)
        {
            _db.GetCollection<CatalogEntity>(CollectionName).InsertOne(CatalogEntity);
        }
        public void Update(CatalogEntity CatalogEntity)
        {
            var filter = Builders<CatalogEntity>.Filter.Where(_ => _.CatalogId == 
            CatalogEntity.CatalogId);
            _db.GetCollection<CatalogEntity>(CollectionName).ReplaceOne(filter, CatalogEntity);
        }
        public void Remove(long id)
        {
            var filter = Builders<CatalogEntity>.Filter.Where(_ => _.CatalogId == id);
            var operation = _db.GetCollection<CatalogEntity>(CollectionName).
                DeleteOne(filter);
        }


    }
}
