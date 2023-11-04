using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

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
