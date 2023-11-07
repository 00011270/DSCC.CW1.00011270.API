using BlogPlatform.Models;
using Microsoft.EntityFrameworkCore;

// Project made by 00011270
// For CC module level 6 WIUT
namespace BlogPlatform.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public BlogContext(DbContextOptions<BlogContext> o) : base(o) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining table constraints for database when migrations happen
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn().HasColumnType("serial");
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn().HasColumnType("serial");
            });
        }

    }
}
