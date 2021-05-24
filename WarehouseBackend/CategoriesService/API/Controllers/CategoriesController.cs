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
    /// <c>CategoriesController</c> is a class.
    /// Contains all http methods for working with categories.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit category.
    /// </remarks>

    // api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// This method returns all categories
        /// </summary>
        /// <response code="200">Returns all categories</response>

        //GET api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            DateTime localDate = DateTime.Now;
            _logger.LogInformation("/api/categories executed at {date}", localDate);
            return Ok(categories);
        }

        /// <summary>
        /// This method returns category that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns category that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/categories/{id}
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                _logger.LogInformation($"/api/categories/id returned category with id {id}");
                return Ok(category);
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
            channel.BasicPublish(exchange: "categories",
                                 routingKey: integrationEvent,
                                 basicProperties: null,
                                 body: body);
        }

        /// <summary>
        /// This method returns category that was created and path to it
        /// </summary>
        /// <response code="201">Returns category that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/categories
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), 201)]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (categoryDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdCategory = await _categoryService.CreateAsync(categoryDto);

                var integrationEventData = JsonConvert.SerializeObject(new
                {
                        id = createdCategory.Id,
                        categoryName = createdCategory.CategoryName
                });
                PublishToMessageQueue("categories.add", integrationEventData);

                _logger.LogInformation($"Category added, id {createdCategory.Id}");

                //Fetch the category from data source
                return CreatedAtRoute("GetCategoryById", new {id = createdCategory.Id}, createdCategory);
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method changes category
        /// </summary>
        /// <response code="204">Returns nothing, category was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that category was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/categories
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _categoryService.Update(categoryDto);

                var integrationEventData = JsonConvert.SerializeObject(new
                {
                        id = categoryDto.Id,
                        categoryName = categoryDto.CategoryName
                });
                PublishToMessageQueue("categories.update", integrationEventData);

                _logger.LogInformation($"Category updated, id {categoryDto.Id}");

                return NoContent();
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method deletes category
        /// </summary>
        /// <response code="204">Returns nothing, category was successfully deleted</response>
        /// <response code="404">Returns message that category was not found</response>

        //DELETE api/categories/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.Remove(id);
                _logger.LogInformation($"Category deleted, id {id}");
                return NoContent();
            }
            catch (DbQueryResultNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}