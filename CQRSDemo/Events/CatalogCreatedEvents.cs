

using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Events
{
    public class CatalogCreatedEvent : IEvent
    {
        public long CatalogId { get; set; }
        public string CatalogName { get; set; }
      
        public List<ProductCreatedEvent> ProductList { get; set; }
        public CatalogEntity ToCatalogEntity()
        {
            return new CatalogEntity
            {
                CatalogId = this.CatalogId,
                CatalogName = this.CatalogName,
                ProductList = (ICollection<Product>)this.ProductList.Select(product => new ProductEntity
                {
                     ProductId = product.ProductNo,
                     ProductDescriptionEntity=product.ProductDescriptionEntity
                }).ToList()
            };
        }
    }
}
