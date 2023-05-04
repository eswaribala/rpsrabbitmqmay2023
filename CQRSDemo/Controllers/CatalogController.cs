using CQRSDemo.Commands;
using CQRSDemo.Events;
using CQRSDemo.Models;
using CQRSDemo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRSDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICommandHandler<Command> _commandHandler;
        private readonly CatalogMongoRepository _mongoRepository;
        private readonly CatalogSqliteRepository _sqliteRepository;
        private readonly CatalogMessageListener _listener;

        public CatalogController(ICommandHandler<Command> commandHandler,
            CatalogSqliteRepository sqliteRepository,
            CatalogMongoRepository repository, CatalogMessageListener listener)
        {
            _commandHandler = commandHandler;
            _sqliteRepository = sqliteRepository;
            _mongoRepository = repository;
            _listener = listener;

            //if (_mongoRepository.GetCustomers().Count == 0)
            //{
            //    var customerCmd = new CreateCustomerCommand
            //    {
            //        Name = "Ajay",
            //        Email = "ajay@email.com",
            //        Age = 23,
            //        Phones = new List<CreatePhoneCommand>
            //        {
            //            new CreatePhoneCommand { Type = PhoneType.CELLPHONE, AreaCode = 123, Number = 7543010 }
            //        }
            //    };
            //    _commandHandler.Execute(customerCmd);

            //}
        }
        [HttpGet]
        public List<CatalogEntity> Get()
        {

            return _mongoRepository.GetCatalogs();
        }
        [HttpGet("{id}", Name = "GetCart")]
        public IActionResult GetById(long id)
        {
            var product = _mongoRepository.GetCatalog(id);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }
        [HttpGet("{email}")]
        public IActionResult GetByName(string name)
        {
            var product = _mongoRepository.GetCatalogByCatalogName(name);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateCatalogCommand cart)
        {
            _commandHandler.Execute(cart);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                _listener.Start();
            }).Start();

            return CreatedAtRoute("GetCart", new { id = cart.Id }, cart);
        }
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] UpdateCatalogCommand cart)
        {
            var record = _sqliteRepository.GetById(id);
            if (record == null)
            {
                return NotFound();
            }
            cart.Id = id;
            _commandHandler.Execute(cart);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var record = _sqliteRepository.GetById(id);
            if (record == null)
            {
                return NotFound();
            }
            _commandHandler.Execute(new DeleteCatalogCommand()
            {
                Id = id
            });
            return NoContent();
        }
    }
}
