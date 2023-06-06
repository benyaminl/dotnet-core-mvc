using Microsoft.EntityFrameworkCore;
using MvcNet.Models;

namespace MvcNet {
    public class AppDBContext : DbContext {
        public AppDBContext(DbContextOptions<AppDBContext> opt)
            : base(opt)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostTagsModel>().HasKey(table => new {
                table.tagId, table.postId
            });
        }
        
        public DbSet<UserModel> user {get; set;} = null!;
        public DbSet<PostModel> posts {get; set;} = null!;
        public DbSet<TagModel> tags {get; set;} = null!;
        public DbSet<PostTagsModel> postTags {get; set;} = null!;
    }
}