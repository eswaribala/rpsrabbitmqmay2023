using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class CreateProductCommand : Command
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
     }
}
