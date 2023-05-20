using DevLogger.Web.Data;
using DevLogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevLogger.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly DevLoggerDbContext _devLoggerDbContext;
        public BlogPostRepository(DevLoggerDbContext devLoggerDbContext)
        {
            _devLoggerDbContext = devLoggerDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _devLoggerDbContext.BlogPosts.AddAsync(blogPost);
            await _devLoggerDbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _devLoggerDbContext.BlogPosts.FindAsync(id);

            if (existingBlogPost != null)
            {
                _devLoggerDbContext.BlogPosts.Remove(existingBlogPost);
                await _devLoggerDbContext.SaveChangesAsync();

                return existingBlogPost;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _devLoggerDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _devLoggerDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost> GetByUrlHandleAsync(string urlHandle)
        {
            return await _devLoggerDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _devLoggerDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Id = blogPost.Id;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Visible = blogPost.Visible;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.Tags = blogPost.Tags;

                await _devLoggerDbContext.SaveChangesAsync();

                return existingBlogPost;
            }

            return null;
        }
    }
}
