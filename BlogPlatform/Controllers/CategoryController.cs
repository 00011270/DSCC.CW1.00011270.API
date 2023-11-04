using BlogPlatform.Models;
using BlogPlatform.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _categoryRepository.GetObjectList());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryRepository.GetObjectById(id);


            return new OkObjectResult(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {

            using (var scope = new TransactionScope())
            {
                await _categoryRepository.InsertObject(category);
                scope.Complete();
                scope.Dispose();
                return CreatedAtAction(nameof(Get), new { ID = category.Id }, category);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            var categoryId = await _categoryRepository.GetObjectById(id);
            if (category != null)
            {
                categoryId.Name = category.Name;
                categoryId.Id = category.Id;

                await _categoryRepository.UpdateObject(categoryId);
                return Ok(categoryId);
            }
            return new NoContentResult();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.DeleteObject(id);
            return new OkResult();
        }
    }
}
