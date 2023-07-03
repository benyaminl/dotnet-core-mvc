using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Domain.Models
{
    public class PostModel {
        [Key]
        public int id {get; set;}
        public string title {get; set;} = null!;
        public string content {get; set;} = null!;
        public DateTime insertDate {get; set;}
        public DateTime publishDate {get; set;}
        
        [ForeignKey("postId")]
        public ICollection<PostTagsModel> blogTags {get; set;} = null!;

        [InverseProperty("post")]
        public ICollection<PostCommentModel> comments { get; set; } = null!;
    }
}