using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// <c>BrandsController</c> is a class.
    /// Contains all http methods for working with brands.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit brands.
    /// </remarks>

    // api/brands
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandsService;
        private readonly ILogger<BrandsController> _logger;


        public BrandsController(IBrandService brandsService, ILogger<BrandsController> logger)
        {
            _brandsService = brandsService;
            _logger = logger;
        }


        /// <summary>
        /// This method returns all brands
        /// </summary>
        /// <response code="200">Returns all brands</response>

        //GET api/brands
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandsService.GetAllAsync();
            DateTime localDate = DateTime.Now;
            _logger.LogInformation("/api/brands executed at {date}", localDate);
            return Ok(brands);
        }


        /// <summary>
        /// This method returns brand that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns brand that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/brands/{id}
        [HttpGet("{id:int}", Name = "GetBrandById")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            try
            {
                var brand = await _brandsService.GetByIdAsync(id);
                _logger.LogInformation($"/api/brands/id returned brand with id {id}");
                return Ok(brand);
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }

        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            // TOOO: Reuse and close connections and channel, etc,
            var factory = new ConnectionFactory() { HostName = "192.168.39.162" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "brands",
                                 routingKey: integrationEvent,
                                 basicProperties: null,
                                 body: body);
        }


        /// <summary>
        /// This method returns brand that was created and path to it
        /// </summary>
        /// <response code="201">Returns brand that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/brands
        [HttpPost]
        [ProducesResponseType(typeof(BrandDto), 201)]
        public async Task<IActionResult> CreateBrand(BrandDto brandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (brandDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdBrand = await _brandsService.CreateAsync(brandDto);

                var integrationEventData = JsonConvert.SerializeObject(new
                {
                    id = createdBrand.Id,
                    brandName = createdBrand.BrandName
                });
                PublishToMessageQueue("brands.add", integrationEventData);

                _logger.LogInformation($"Brand added, id {createdBrand.Id}");

                //Fetch the brand from data source
                return CreatedAtRoute("GetBrandById", new { id = createdBrand.Id }, createdBrand);
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }


        /// <summary>
        /// This method changes brand
        /// </summary>
        /// <response code="204">Returns nothing, brand was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that brand was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/brands
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateBrand(BrandDto brandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _brandsService.Update(brandDto);

                var integrationEventData = JsonConvert.SerializeObject(new
                {
                    id = brandDto.Id,
                    brandName = brandDto.BrandName
                });
                PublishToMessageQueue("brands.update", integrationEventData);

                _logger.LogInformation($"Brand updated, id {brandDto.Id}");

                return NoContent();
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }


        /// <summary>
        /// This method deletes brand
        /// </summary>
        /// <response code="204">Returns nothing, brand was successfully deleted</response>
        /// <response code="404">Returns message that brand was not found</response>

        //DELETE api/brands/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                _brandsService.Remove(id);
                _logger.LogInformation($"Brand deleted, id {id}");
                return NoContent();
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}