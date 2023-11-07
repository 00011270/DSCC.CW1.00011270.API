using BlogPlatform.Models;

namespace BlogPlatform.Repository
  
{
    // CategoryRepository interface that has GetPostsByCategoryId
    //method and it also implements IRepository interface
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Post>> GetPostsByCategoryId(int categoryId);
    }
}
