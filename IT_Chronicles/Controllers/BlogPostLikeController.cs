using IT_Chronicles.Data;
using IT_Chronicles.Models.Domain;
using IT_Chronicles.Models.ViewModels;
using IT_Chronicles.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Chronicles.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostLikeController : ControllerBase
	{
		private readonly IBlogPostLikeRepository blogPostLikeRepository;

		public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
			this.blogPostLikeRepository = blogPostLikeRepository;
		}

        // Add like 
        [HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
		{
			var blogPostLike = new BlogPostLike
			{
				BlogPostId = addLikeRequest.BlogPostId,
				UserId = addLikeRequest.UserId
			};

			await blogPostLikeRepository.AddLike(blogPostLike);
			
			return Ok();
		}

		[HttpGet]
		[Route("{blogPostId:Guid}/totalLikes")]
		public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
		{

			var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPostId);

			return Ok(totalLikes);

		}
	}
}
