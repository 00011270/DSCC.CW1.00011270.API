using BlogPlatform.DAL;
using BlogPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Repository
{
    //Category class that implements ICategoryRepository
    public class CategoryRepository : ICategoryRepository
    {
        // Defining Context with Dep Injec
        private readonly BlogContext blogContext;

        public CategoryRepository(BlogContext blogContext)
        {
            this.blogContext = blogContext;
        }

        //Asynchronously Deletes Category object based on its ID
        public async Task DeleteObject(int objId)
        {
            // Gets the Category from Database based on given id
            var obj = blogContext.Set<Category>().Find(objId);
            // Removes the retrieved category from database
            blogContext.Set<Category>().Remove(obj);
            // Asynchronously Saving the changes to database
            await blogContext.SaveChangesAsync();
        }

        // Asynchronously Getting the Category by its id
        public async Task<Category> GetObjectById(int obj)
        {
            // Gets the Category from database based on given id
            // And returns the result
            return await blogContext.Set<Category>().FindAsync(obj);
        }

        // Asynchronously Getting the Category List from database
        public async Task<IEnumerable<Category>> GetObjectList()
        {
            // Gets All categories from database asynchronously
            // and returns the List of Categories
            return await blogContext.Set<Category>().ToListAsync();
        }

        // Creating a category and saving it in database
        public async Task InsertObject(Category obj)
        {
            await blogContext.Set<Category>().AddAsync(obj);
            await blogContext.SaveChangesAsync();
        }


        // Updating the category object 
        public async Task UpdateObject(Category obj)
        {
            blogContext.Set<Category>().Update(obj);
            await blogContext.SaveChangesAsync();
        }


        // Gets the categoryId that was sent from frontend and returns
        // the list of Posts to the controller
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
