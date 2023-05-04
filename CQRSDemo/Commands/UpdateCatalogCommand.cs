using CQRSDemo.Events;
using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class UpdateCatalogCommand : Command
    {
        public string CatalogName { get; set; }
       
        public List<CreateProductCommand> ProductList { get; set; }
        public CatalogUpdatedEvent ToCartEvent()
        {
            return new CatalogUpdatedEvent
            {
               CatalogId=this.Id,
               CatalogName=this.CatalogName,

               ProductList = this.ProductList.Select(product => new ProductCreatedEvent
                {
                   ProductName=product.ProductName,
                   ProductNo=this.Id
                }).ToList()
            };
        }
        public Catalog ToCartRecord(Catalog record)
        {
            record.CatalogName = this.CatalogName;
            record.ProductList = this.ProductList.Select(product => new Product
            {
                
                ProductId=product.ProductId,
             ProductName=product.ProductName   

            }).ToList();
                
            return record;
        }
    }
}
