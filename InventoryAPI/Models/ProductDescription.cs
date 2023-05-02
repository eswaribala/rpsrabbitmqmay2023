using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
    public enum ProductType { SEASONAL,REGULAR}
    [Owned]
    public class ProductDescription
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [Column("Purchase_Date")]
        public DateTime DOP { get; set; }
        [Column("Description")]
        public string? Description { get; set; }
        [Column("Product_Type")]
        public ProductType ProductType { get; set; }
        [Column("Unit_Price")]
        public long UnitPrice { get; set; }
    }
}
