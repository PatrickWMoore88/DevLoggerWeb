using DevLogger.Web.Data;
using DevLogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevLogger.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DevLoggerDbContext _devLoggerDbContext;
        public TagRepository(DevLoggerDbContext devLoggerDbContext)
        {
            _devLoggerDbContext = devLoggerDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _devLoggerDbContext.Tags.AddAsync(tag);
            await _devLoggerDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await _devLoggerDbContext.Tags.FindAsync(id);

            if(existingTag != null)
            {
                _devLoggerDbContext.Tags.Remove(existingTag);
                await _devLoggerDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _devLoggerDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await _devLoggerDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _devLoggerDbContext.Tags.FindAsync(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _devLoggerDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
