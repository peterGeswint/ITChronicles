using IT_Chronicles.Data;
using IT_Chronicles.Models.Domain;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<int> CountAsync()
        {
           return await iTChroniclesDbContext.Tags.CountAsync();
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

        public async Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery, string? sortBy, string sortDirection, int pageNumber, int pageSize)
        {
            var query = iTChroniclesDbContext.Tags.AsQueryable(); 

            //filtering
            if(string.IsNullOrWhiteSpace(searchQuery) == false)
            {

                query = query.Where(x => x.Name.Contains(searchQuery) || x.DisplayName.Contains(searchQuery));

            }

            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }

                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                }

            }

            //pagination

            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);



            return await query.ToListAsync();
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
