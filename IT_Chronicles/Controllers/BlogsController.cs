using IT_Chronicles.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
       {
            this.blogPostRepository = blogPostRepository;
        }


        // GET: BlogsController
        public async Task<ActionResult> Index( string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            return View(blogPost);
        }

    }
}
