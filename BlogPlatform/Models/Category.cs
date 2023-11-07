using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

// Project made by 00011270
// For CC module level 6 WIUT
namespace BlogPlatform.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
