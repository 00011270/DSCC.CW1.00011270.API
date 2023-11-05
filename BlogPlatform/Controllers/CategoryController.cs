﻿using BlogPlatform.Models;
using BlogPlatform.Repository;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
//using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet("{categoryId}/posts")]
        public async Task<IActionResult> GetPostsByCategory(int categoryId)
        {
            var posts = await _categoryRepository.GetPostsByCategoryId(categoryId);
            return new OkObjectResult(posts);
        }

    }
}
