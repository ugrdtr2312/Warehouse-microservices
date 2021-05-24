using System;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace API.Controllers
{
    /// <summary>
    /// <c>SuppliersController</c> is a class.
    /// Contains all http methods for working with suppliers.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit suppliers.
    /// </remarks>

    // api/suppliers
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(ISupplierService supplierService, ILogger<SuppliersController> logger)
        {
            _supplierService = supplierService;
            _logger = logger;
        }

        /// <summary>
        /// This method returns all suppliers
        /// </summary>
        /// <response code="200">Returns all suppliers</response>
        //GET api/suppliers
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllAsync();
            DateTime localDate = DateTime.Now;
            _logger.LogInformation("/api/suppliers executed at {date}", localDate);
            return Ok(suppliers);
        }

        /// <summary>
        /// This method returns supplier that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns supplier that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/suppliers/{id}
        [HttpGet("{id:int}", Name = "GetSupplierById")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetByIdAsync(id);
                _logger.LogInformation($"/api/suppliers/id returned supplier with id {id}");
                return Ok(supplier);
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }

        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            // TOOO: Reuse and close connections and channel, etc,
            var factory = new ConnectionFactory() {HostName = "192.168.39.162"};
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "suppliers",
                                 routingKey: integrationEvent,
                                 basicProperties: null,
                                 body: body);
        }

        /// <summary>
        /// This method returns supplier that was created and path to it
        /// </summary>
        /// <response code="201">Returns supplier that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/suppliers
        [HttpPost]
        [ProducesResponseType(typeof(SupplierDto), 201)]
        public async Task<IActionResult> CreateSupplier(SupplierDto supplierDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (supplierDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdSupplier = await _supplierService.CreateAsync(supplierDto);

                var integrationEventData = JsonConvert.SerializeObject(new
                {
                        id = createdSupplier.Id,
                        companyName = createdSupplier.CompanyName
                });
                PublishToMessageQueue("suppliers.add", integrationEventData);

                _logger.LogInformation($"Supplier added, id {createdSupplier.Id}");

                //Fetch the supplier from data source
                return CreatedAtRoute("GetSupplierById", new {id = createdSupplier.Id}, createdSupplier);
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method changes supplier
        /// </summary>
        /// <response code="204">Returns nothing, supplier was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that supplier was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/suppliers
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateSupplier(SupplierDto supplierDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _supplierService.Update(supplierDto);

                var integrationEventData = JsonConvert.SerializeObject(new
                {
                        id = supplierDto.Id,
                        companyName = supplierDto.CompanyName
                });
                PublishToMessageQueue("suppliers.update", integrationEventData);

                _logger.LogInformation($"Supplier updated, id {supplierDto.Id}");

                return NoContent();
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method deletes supplier
        /// </summary>
        /// <response code="204">Returns nothing, supplier was successfully deleted</response>
        /// <response code="404">Returns message that supplier was not found</response>

        //DELETE api/suppliers/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                _supplierService.Remove(id);
                _logger.LogInformation($"Supplier deleted, id {id}");
                return NoContent();
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}