using IT_Chronicles.Models.Domain;

namespace IT_Chronicles.Models.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<BlogPost> BlogPosts {get; set;}

		public IEnumerable<Tag> Tags {get; set;}
	}
}