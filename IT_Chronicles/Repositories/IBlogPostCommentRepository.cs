using IT_Chronicles.Models.Domain;

namespace IT_Chronicles.Repositories
{
	public interface IBlogPostCommentRepository
	{
		Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

		Task<IEnumerable<BlogPostComment>> GetCommentByIdAsync( Guid blogPostId);
	}
}
