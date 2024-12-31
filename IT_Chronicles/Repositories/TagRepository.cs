using IT_Chronicles.Data;
using IT_Chronicles.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IT_Chronicles.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ITChroniclesDbContext iTChroniclesDbContext;

        public TagRepository( ITChroniclesDbContext iTChroniclesDbContext)
        {
            this.iTChroniclesDbContext = iTChroniclesDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await iTChroniclesDbContext.Tags.AddAsync(tag);
            await iTChroniclesDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            
           var existingTag = await iTChroniclesDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                iTChroniclesDbContext.Tags.Remove(existingTag);
                await iTChroniclesDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await iTChroniclesDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
          return iTChroniclesDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await iTChroniclesDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null) 
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await iTChroniclesDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
