using BlogPlatform.Models;

namespace BlogPlatform.Repository
  
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Post>> GetPostsByCategoryId(int categoryId);
    }
}
