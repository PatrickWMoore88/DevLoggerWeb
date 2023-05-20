using DevLogger.Web.Data;
using DevLogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevLogger.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly DevLoggerDbContext devLoggerDbContext;

        public BlogPostCommentRepository(DevLoggerDbContext devLoggerDbContext)
        {
            this.devLoggerDbContext = devLoggerDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await devLoggerDbContext.BlogPostComment.AddAsync(blogPostComment);
            await devLoggerDbContext.SaveChangesAsync();

            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blogPostId)
        {
            return await devLoggerDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
