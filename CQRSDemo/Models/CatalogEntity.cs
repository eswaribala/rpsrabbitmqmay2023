using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CQRSDemo.Models
{
   
    public class CatalogEntity
    {
        
       [BsonId]
        public long CatalogId { get; set; }
        [BsonElement]
        public string? CatalogName { get; set; }
        [JsonIgnore]
        [BsonElement]
        public ICollection<Product> ProductList;
    }
}
