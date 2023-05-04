using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Events
{
    public class CatalogUpdatedEvent : IEvent
    {
        public long CatalogId { get; set; }
        public string CatalogName { get; set; }
      
        public List<ProductCreatedEvent> ProductList{ get; set; }
        public CatalogEntity ToCartEntity(CatalogEntity CatalogEntity)
        {
            return new CatalogEntity
            {
                CatalogId = this.CatalogId,
                CatalogName = CatalogEntity.CatalogName.Equals(this.CatalogName) ? CatalogEntity.CatalogName : this.CatalogName,
                ProductList = GetNewOnes(CatalogEntity.ProductList)
                .Select(product => new ProductEntity { 
                    ProductId = product.ProductId ,
                    ProductName = product.ProductName  
                  
                   }).ToList()
            };
        }
        private List<ProductEntity> GetNewOnes(List<ProductEntity> ProductList)
        {
            return ProductList.Where(a => !this.ProductList.Any(x => x.ProductNo== a.ProductId
                && x.ProductName == a.ProductName
                )).ToList<ProductEntity>();
        }
    }
}
