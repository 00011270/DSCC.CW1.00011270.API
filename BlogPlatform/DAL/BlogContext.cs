using BlogPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public BlogContext(DbContextOptions<BlogContext> o) : base(o) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
