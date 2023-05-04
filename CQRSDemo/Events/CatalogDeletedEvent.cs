using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Events
{
    public class CatalogDeletedEvent : IEvent
    {
        public long CatalogId { get; set; }
    }
}
