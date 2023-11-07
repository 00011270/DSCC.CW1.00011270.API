using BlogPlatform.Models;

// Project made by 00011270
// For CC module level 6 WIUT
namespace BlogPlatform.Repository
  
{
    // CategoryRepository interface that has GetPostsByCategoryId
    //method and it also implements IRepository interface
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Post>> GetPostsByCategoryId(int categoryId);
    }
}
