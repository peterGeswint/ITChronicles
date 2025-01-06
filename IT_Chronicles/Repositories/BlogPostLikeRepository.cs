using Microsoft.EntityFrameworkCore;
using IT_Chronicles.Data;
using IT_Chronicles.Models.Domain;

namespace IT_Chronicles.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly ITChroniclesDbContext iTChroniclesDbContext;

        public BlogPostLikeRepository(ITChroniclesDbContext iTChroniclesDbContext)
        {
            this.iTChroniclesDbContext = iTChroniclesDbContext;
        }

		public async Task<BlogPostLike> AddLike(BlogPostLike blogPostLike)
		{
			await iTChroniclesDbContext.blogPostLike.AddAsync(blogPostLike);
            await iTChroniclesDbContext.SaveChangesAsync();
            return blogPostLike;
		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
        {
          return await iTChroniclesDbContext.blogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
