using IT_Chronicles.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using IT_Chronicles.Models.ViewModels;

namespace IT_Chronicles.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogsController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository)
       {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
        }


        // GET: BlogsController
        public async Task<ActionResult> Index(string urlHandle)
        {

            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();

            if (blogPost != null)
            {
                var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,

                };
            }
            return View(blogDetailsViewModel);
        }

    }
}
