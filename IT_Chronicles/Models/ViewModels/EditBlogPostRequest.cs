﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Chronicles.Models.ViewModels
{
    public class EditBlogPostRequest
    {

        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        //Display the Tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Select a tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
