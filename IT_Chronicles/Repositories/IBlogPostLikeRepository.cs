using IT_Chronicles.Models.Domain;

namespace IT_Chronicles.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<BlogPostLike> AddLike(BlogPostLike blogPostLike);
    }
}
