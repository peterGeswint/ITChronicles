using IT_Chronicles.Data;
using IT_Chronicles.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IT_Chronicles.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ITChroniclesDbContext iTChroniclesDbContext;

        public BlogPostRepository( ITChroniclesDbContext iTChroniclesDbContext)
        {
            this.iTChroniclesDbContext = iTChroniclesDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await iTChroniclesDbContext.AddAsync(blogPost);
            await iTChroniclesDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
           var existingBlog = await iTChroniclesDbContext.BlogPosts.FindAsync(id);

            if(existingBlog != null)
            {
                iTChroniclesDbContext.BlogPosts.Remove(existingBlog);
                await iTChroniclesDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection)
        {
            var query = iTChroniclesDbContext.BlogPosts.Include(x => x.Tags).AsQueryable();
            
            //filtering
            if(string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Heading.Contains(searchQuery) || x.PageTitle.Contains(searchQuery));
            }

            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDec = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if(string.Equals(sortBy, "Heading", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDec ? query.OrderByDescending(x => x.Heading) : query.OrderBy(x => x.Heading);
                }

                if (string.Equals(sortBy, "PageTitle", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDec ? query.OrderByDescending(x => x.PageTitle) : query.OrderBy(x => x.PageTitle);
                }

            }

            //pagination

            return await query.ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return await iTChroniclesDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await iTChroniclesDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await iTChroniclesDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog != null) 
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await iTChroniclesDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
            
        }   
    }
}
