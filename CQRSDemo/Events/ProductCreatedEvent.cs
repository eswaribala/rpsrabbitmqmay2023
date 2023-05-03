using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Events
{
    public class ProductCreatedEvent : IEvent
    {
    
        public long ProductNo { get; set; }
        public ProductDescriptionEntity ProductDescriptionEntity { get; set; }
        
    }
}
