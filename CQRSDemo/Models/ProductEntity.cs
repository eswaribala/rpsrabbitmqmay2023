using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSDemo.Models
{

    public class ProductEntity
    {
        [BsonId]
        public long ProductId { get; set; }
        public ProductDescriptionEntity? ProductDescription { get; set; }
      
    }
}
