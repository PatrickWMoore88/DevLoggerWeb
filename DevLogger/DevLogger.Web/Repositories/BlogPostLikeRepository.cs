using DevLogger.Web.Data;
using DevLogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevLogger.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly DevLoggerDbContext devLoggerDbContext;

        public BlogPostLikeRepository(DevLoggerDbContext devLoggerDbContext)
        {
            this.devLoggerDbContext = devLoggerDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await devLoggerDbContext.BlogPostLike.AddAsync(blogPostLike);
            await devLoggerDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await devLoggerDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await devLoggerDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
