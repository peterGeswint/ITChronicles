using IT_Chronicles.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IT_Chronicles.Data
{
    public class ITChroniclesDbContext : DbContext
    {
        public ITChroniclesDbContext(DbContextOptions<ITChroniclesDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogPostLike> blogPostLike { get; set; }

        public DbSet<BlogPostComment> blogPostComments { get; set; }
    }
}
