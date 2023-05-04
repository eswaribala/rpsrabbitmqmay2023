using CQRSDemo.Events;
using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class CreateCatalogCommand : Command
    {
        public string CatalogName { get; set; }
        
        public List<CreateProductCommand> ProductList { get; set; }
        public CatalogCreatedEvent ToCartEvent(long id)
        {
            return new CatalogCreatedEvent
            {
                CatalogId = id,
                CatalogName = this.CatalogName,
                
                ProductList = this.ProductList.Select(product => 
                new ProductCreatedEvent {
                    ProductName=product.ProductName,
                   
                    ProductNo=product.ProductId
                }).ToList()
            };
        }
        public Catalog ToCatalogRecord()
        {
            return new Catalog
            {
                CatalogName = this.CatalogName,
               ProductList= this.ProductList.Select
                (product => new Product{ ProductName=product.ProductName,
                  ProductId=product.ProductId,
                }).ToList()
            };
        }
    }
}
