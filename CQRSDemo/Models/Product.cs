using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSDemo.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        [Column("Product_Id")]
        public long ProductId { get; set; }
        public ProductDescription? ProductDescription { get; set; }
        [ForeignKey("Catalog")]
        [Column("Catalog_Id_FK")]
        public long CatalogId { get; set; } 
        public Catalog? Catalog { get; set; }
    }
}
