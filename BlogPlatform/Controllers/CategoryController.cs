using BlogPlatform.Models;
using BlogPlatform.Repository;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
//using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// Project made by 00011270
// For CC module level 6 WIUT
namespace BlogPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        // Gets the Category List by invoking repository method
        // And returns the result in HTTP response
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _categoryRepository.GetObjectList());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        // Gets the specific Category by its ID by invoking GetObjectById method from repository
        // and sends the Http response with the object
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryRepository.GetObjectById(id);


            return new OkObjectResult(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        // Gets the category object from body and sends that object to the repository
        // and returns the result with CreatedAtAction with response code of 201
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            try
            {
                await _categoryRepository.InsertObject(category);
                return CreatedAtAction(nameof(Get), new { ID = category.Id }, category);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        // Gets the id and category object from body of the request header
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            // First it gets the category by the given id
            var categoryId = await _categoryRepository.GetObjectById(id);

            // If the category is in the Database then changing the properties of it
            if (category != null)
            {
                categoryId.Name = category.Name;
                categoryId.Id = category.Id;

                // Sending the updated object to database
                await _categoryRepository.UpdateObject(categoryId);
                return Ok(categoryId);
            }
            return new NoContentResult();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        // Gets the id for deleting object
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.DeleteObject(id);
            return new OkResult();
        }

        [HttpGet("{categoryId}/posts")]
        // Gets the categoryId that was sent by MVC controller
        // then calls GetPostsByCategoryId method from repository
        // and gets Posts related to category and sends it
        public async Task<IActionResult> GetPostsByCategory(int categoryId)
        {
            var posts = await _categoryRepository.GetPostsByCategoryId(categoryId);
            return new OkObjectResult(posts);
        }

    }
}
