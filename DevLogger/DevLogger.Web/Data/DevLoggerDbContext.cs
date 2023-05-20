using DevLogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevLogger.Web.Data
{
    public class DevLoggerDbContext : DbContext
    {
        public DevLoggerDbContext(DbContextOptions<DevLoggerDbContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
        public DbSet<BlogPostComment> BlogPostComment { get; set; }
    }
}
