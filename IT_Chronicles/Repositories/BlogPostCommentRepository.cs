using IT_Chronicles.Data;
using IT_Chronicles.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IT_Chronicles.Repositories
{
	public class BlogPostCommentRepository : IBlogPostCommentRepository
	{
		private readonly ITChroniclesDbContext iTChroniclesDbContext;

		public BlogPostCommentRepository(ITChroniclesDbContext iTChroniclesDbContext)
        {
			this.iTChroniclesDbContext = iTChroniclesDbContext;
		}

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
		{
			await iTChroniclesDbContext.blogPostComments.AddAsync(blogPostComment);
			await iTChroniclesDbContext.SaveChangesAsync();
			return blogPostComment;
		}

		public async Task<IEnumerable<BlogPostComment>> GetCommentByIdAsync(Guid blogPostId)
		{
			return await iTChroniclesDbContext.blogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
		}
	}
}
