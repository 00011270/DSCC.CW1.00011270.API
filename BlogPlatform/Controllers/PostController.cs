using BlogPlatform.Repository;
using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Models;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// Project made by 00011270
// For CC module level 6 WIUT
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
        // Gets the Post List by invoking repository method
        // And returns the result in HTTP response
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await postRepository.GetObjectList());
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        // Gets the specific Post by its ID by invoking GetObjectById method from repository
        // and sends the Http response with the object
        public async Task<IActionResult> Get(int id)
        {
            var post = await postRepository.GetObjectById(id);

            return new OkObjectResult(post);
        }

        // POST api/<PostController>
        [HttpPost]

        // Gets the post object from body and sends that object to the repository
        // and returns the result with CreatedAtAction with response code of 201
        public async Task<IActionResult> Post([FromBody] Post post)
        {
                try
                {
                    await postRepository.InsertObject(post);
                    return CreatedAtAction(nameof(Get), new { ID = post.Id }, post);
                }catch (Exception ex)
                {
                    // If insertion is incorrect then 500 error is send
                    return StatusCode(500, "Internal Server error " + ex.Message);
                }
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        // Gets the id and post object from body of the request header
        public async Task<IActionResult> Put(int id, [FromBody] Post post)
        {
            // First it gets the post by the given id
            var getPostById = await postRepository.GetObjectById(id);

            // If the post is in the Database then changing the properties of it
            if(getPostById != null)
            {
                getPostById.Id = post.Id; 
                getPostById.Title = post.Title;
                getPostById.Content = post.Content;
                getPostById.CategoryId = post.CategoryId;

                // Sending the updated object to database
                await postRepository.UpdateObject(getPostById);
                return Ok(getPostById);
            }
            return new NoContentResult();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        // Gets the id for deleting object
        public async Task<IActionResult> Delete(int id)
        {
            await postRepository.DeleteObject(id);
            return new OkResult();
        }
    }
}
