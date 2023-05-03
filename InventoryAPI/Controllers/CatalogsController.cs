using InventoryAPI.Models;
using InventoryAPI.Producers;
using InventoryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private ICatalogRepository catalogRepository;
        private IRabbitmqProducer rabbitmqProducer;

        public CatalogsController(ICatalogRepository _catalogRepository,
            IRabbitmqProducer _rabbitmqProducer)
        {
            this.catalogRepository = _catalogRepository;
            this.rabbitmqProducer = _rabbitmqProducer;
        }

        [HttpGet("cataloglist")]
        public IEnumerable<Catalog> CatalogList()
        {
            var catalogList = catalogRepository.GetCatalogList();
            return catalogList;
        }
        [HttpGet("getcatalogbyid")]
        public Catalog GetCatalogById(int Id)
        {
            return catalogRepository.GetCatalogById(Id);
        }
        [HttpPost("addcatalog")]
        public Catalog AddProduct(Catalog catalog)
        {
            var catalogData = catalogRepository.AddCatalog(catalog);
            //send the inserted product data to the queue and consumer will listening this data from queue
            rabbitmqProducer.SendMessage(catalogData);
            return catalogData;
        }
        [HttpPut("updatecatalog")]
        public Catalog UpdateCatalog(Catalog catalog)
        {
            return catalogRepository.UpdateCatalog(catalog);
        }
        [HttpDelete("deletecatalog")]
        public bool DeleteCatalog(int Id)
        {
            return catalogRepository.DeleteCatalog(Id);
        }
    }
}
