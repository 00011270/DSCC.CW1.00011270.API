using BlogPlatform.DAL;
using BlogPlatform.Models;
using Microsoft.EntityFrameworkCore;

// Project made by 00011270
// For CC module level 6 WIUT
namespace BlogPlatform.Repository
{
    public class PostRepository : IRepository<Post>
    {
        // Defining Context with Dep Injec
        private readonly BlogContext _blogContext;

        public PostRepository(BlogContext blogContext)
        {
            this._blogContext = blogContext;
        }

        //Asynchronously Deletes Post object based on its ID
        public async Task DeleteObject(int objId)
        {
            // Gets the Post from Database based on given id
            var obj = _blogContext.Set<Post>().Find(objId);
            // Removes the retrieved post from database
            _blogContext.Set<Post>().Remove(obj);
            // Asynchronously Saving the changes to database
            await _blogContext.SaveChangesAsync();
        }

        // Asynchronously Getting the Post by its id
        public async Task<Post> GetObjectById(int obj)
        {
            // Gets the Post from database based on given id
            // And returns the result
            return await _blogContext.Set<Post>().FindAsync(obj);
        }

        // Asynchronously Getting the Post List from database
        public async Task<IEnumerable<Post>> GetObjectList()
        {
            // Gets All posts from database asynchronously
            // and returns the List of POsts
            return await _blogContext.Set<Post>().ToListAsync();
        }

        // Creating a post and saving it in database
        public async Task InsertObject(Post obj)
        {
            await _blogContext.Set<Post>().AddAsync(obj);
            await _blogContext.SaveChangesAsync();
        }

        // Updating the post object 
        public async Task UpdateObject(Post obj)
        {
            _blogContext.Set<Post>().Update(obj);
            await _blogContext.SaveChangesAsync();
        }

    }

}
