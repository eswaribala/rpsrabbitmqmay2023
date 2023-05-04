using CQRSDemo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class DeleteCatalogCommand : Command
    {
        internal CatalogDeletedEvent ToCartEvent()
        {
            return new CatalogDeletedEvent
            {
                CatalogId = this.Id
            };
        }
    }
}
