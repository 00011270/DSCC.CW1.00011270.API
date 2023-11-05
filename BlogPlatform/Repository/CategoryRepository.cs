using BlogPlatform.DAL;
using BlogPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly BlogContext blogContext;

        public CategoryRepository(BlogContext blogContext)
        {
            this.blogContext = blogContext;
        }
        public async Task DeleteObject(int objId)
        {
            var obj = blogContext.Set<Category>().Find(objId);
            blogContext.Set<Category>().Remove(obj);
            await blogContext.SaveChangesAsync();
        }

        public async Task<Category> GetObjectById(int obj)
        {
            return await blogContext.Set<Category>().FindAsync(obj);
        }

        public async Task<IEnumerable<Category>> GetObjectList()
        {
            return await blogContext.Set<Category>().ToListAsync();
        }


        public async Task InsertObject(Category obj)
        {
            await blogContext.Set<Category>().AddAsync(obj);
            await blogContext.SaveChangesAsync();
        }

        public async Task UpdateObject(Category obj)
        {
            blogContext.Set<Category>().Update(obj);
            await blogContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryId(int categoryId)
        {
            return await blogContext.Set<Category>()
                .Where(c => c.Id == categoryId)
                .Include(c => c.Posts)
                .SelectMany(c => c.Posts)
                .ToListAsync();
        }
    }
}
