using BlogPlatform.Repository;
using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Models;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepository<Post> postRepository;

        public PostController(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }
        // GET: api/<PostController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await postRepository.GetObjectList());
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await postRepository.GetObjectById(id);

            return new OkObjectResult(post);
        }

        // POST api/<PostController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
                try
                {
                    await postRepository.InsertObject(post);
                    return CreatedAtAction(nameof(Get), new { ID = post.Id }, post);
                }catch (Exception ex)
                {
                    return StatusCode(500, "Internal Server error " + ex.Message);
                }
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Post post)
        {
            var getPostById = await postRepository.GetObjectById(id);
            if(getPostById != null)
            {
                getPostById.Id = post.Id; 
                getPostById.Title = post.Title;
                getPostById.Content = post.Content;
                getPostById.CategoryId = post.CategoryId;

                await postRepository.UpdateObject(getPostById);
                return Ok(getPostById);
            }
            return new NoContentResult();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await postRepository.DeleteObject(id);
            return new OkResult();
        }
    }
}
