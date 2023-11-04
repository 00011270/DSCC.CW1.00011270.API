using BlogPlatform.DAL;
using BlogPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Repository
{
    public class PostRepository : IRepository<Post>
    { 
        private readonly BlogContext _blogContext;

        public PostRepository(BlogContext blogContext)
        {
            this._blogContext = blogContext;
        }

        public async Task DeleteObject(int objId)
        {
            var obj = _blogContext.Set<Post>().Find(objId);
            _blogContext.Set<Post>().Remove(obj);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<Post> GetObjectById(int obj)
        {
            return await _blogContext.Set<Post>().FindAsync(obj);
        }

        public async Task<IEnumerable<Post>> GetObjectList()
        {
            return await _blogContext.Set<Post>().ToListAsync();
        }

        public async Task InsertObject(Post obj)
        {
            await _blogContext.Set<Post>().AddAsync(obj);
            await _blogContext.SaveChangesAsync();
        }

        public async Task UpdateObject(Post obj)
        {
            _blogContext.Set<Post>().Update(obj);
            await _blogContext.SaveChangesAsync();
        }
    }

}
