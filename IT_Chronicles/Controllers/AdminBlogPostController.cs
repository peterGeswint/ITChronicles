using IT_Chronicles.Models.Domain;
using IT_Chronicles.Models.ViewModels;
using IT_Chronicles.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Chronicles.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tag = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tag.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //mapping domain to request
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };

            //Map tags to selected tags
            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {

                    selectedTags.Add(existingTag);

                }

                // map tag to blogpost domain model.
                blogPost.Tags = selectedTags;
            }

            await blogPostRepository.AddAsync(blogPost);
            TempData["successMessage"] = "BlogPost was created succesfully";
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(string? searchQuery,string? sortBy, string? sortDirection)
        {
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            // call repository
            var blogPost = await blogPostRepository.GetAllAsync(searchQuery, sortBy, sortDirection);

            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //retireve the result from repository.
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if(blogPost != null)
            {
                // Pass data to view.
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(blogPost);
            }

            return View(null);
          
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                Author = editBlogPostRequest.Author,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,
                PublishedDate = editBlogPostRequest.PublishedDate,
               


            };
            //map tags 
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag)) 
                {
                   var foundTag = await tagRepository.GetAsync(tag);

                    if(foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }

            }

            blogPostDomainModel.Tags = selectedTags;

            //submit tags
           var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if(updatedBlog != null)
            {
                TempData["successMessage"] = "BlogPost Updated successfully";
                return RedirectToAction("List");
            }
            TempData["errorMessage"] = "Something went wrong";
            return RedirectToAction("Edit");
           
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            // talking to the repository to delete the tags and blogpost
           var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if(deletedBlogPost != null)
            {
                TempData["successMessage"] = "BlogPost deleted successfully";
                return RedirectToAction("List");
            }
            //getting the  failure response
            TempData["errorMessage"] = "Something went wrong";
            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }
           
           
    }
}

