using IT_Chronicles.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using IT_Chronicles.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using IT_Chronicles.Models.Domain;

namespace IT_Chronicles.Controllers
{
	public class BlogsController : Controller
	{
		private readonly IBlogPostRepository blogPostRepository;
		private readonly IBlogPostLikeRepository blogPostLikeRepository;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IBlogPostCommentRepository blogPostCommentRepository;

		public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostCommentRepository)
		{
			this.blogPostRepository = blogPostRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.blogPostCommentRepository = blogPostCommentRepository;
		}


		// GET: BlogsController
		[HttpGet]
		public async Task<IActionResult> Index(string urlHandle)
		{

			var liked = false;
			var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
			var blogDetailsViewModel = new BlogDetailsViewModel();

			if (blogPost != null)
			{
				var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

				if (signInManager.IsSignedIn(User))
				{
					//Get like for this blogpost
					var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

					var userId = userManager.GetUserId(User);

					if (userId != null)
					{
						var likesFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
						liked = likesFromUser != null;
					}
				}

				var blogComments = await blogPostCommentRepository.GetCommentByIdAsync(blogPost.Id);

				var blogCommentsForView = new List<BlogComment>();

				foreach (var blogComment in blogComments) 
				{
					blogCommentsForView.Add(new BlogComment
					{
						Description = blogComment.Description,
						DateAdded = blogComment.DateAdded,
						UserName = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
					});

				}

				blogDetailsViewModel = new BlogDetailsViewModel
				{
					Id = blogPost.Id,
					Content = blogPost.Content,
					PageTitle = blogPost.PageTitle,
					Author = blogPost.Author,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					Heading = blogPost.Heading,
					PublishedDate = DateTime.Now,
					ShortDescription = blogPost.ShortDescription,
					UrlHandle = blogPost.UrlHandle,
					Visible = blogPost.Visible,
					Tags = blogPost.Tags,
					TotalLikes = totalLikes,
					Liked = liked,
					Comments = blogCommentsForView

				};
			}
			return View(blogDetailsViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
		{
			if (signInManager.IsSignedIn(User))
			{
				var domainModel = new BlogPostComment
				{
					BlogPostId = blogDetailsViewModel.Id,
					Description = blogDetailsViewModel.CommentDescription,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};

				await blogPostCommentRepository.AddAsync(domainModel);
				return RedirectToAction("Index","Home",new {urlHandle = blogDetailsViewModel.UrlHandle});
			}

			return View();

		}

	}
}
