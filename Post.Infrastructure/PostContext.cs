using Microsoft.EntityFrameworkCore;
using Post.Domain.Models;

namespace Post.Infrastructure {
    public class PostContext : DbContext {
        public PostContext(DbContextOptions<PostContext> opt)
            : base(opt)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostTagsModel>().HasKey(table => new {
                table.tagId, table.postId
            });
        }

        public DbSet<PostModel> posts {get; set;} = null!;
        public DbSet<PostTagsModel> postTags {get; set;} = null!;
        public DbSet<PostCommentModel> commentModels {get; set;} = null!;
    }
}