using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSDemo.Models
{
    public enum ProductTypeEntity { SEASONAL,REGULAR}
    
    public class ProductDescriptionEntity
    {
        [BsonElement]
        public DateTime DOP { get; set; }
        [BsonElement]
        public string? Description { get; set; }
        [BsonElement]
        public ProductType ProductType { get; set; }
        [BsonElement]
        public long UnitPrice { get; set; }
    }
}
