using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNet.Models
{
    [Table(name: "post_comments")]
    public class PostCommentModel
    {
        [Key]
        public int id {get; set;}
        public string name {get; set;} = null!;
        public string email {get; set;} = null!;
        public string content {get; set;} = null!;
        public DateTime insertDate {get; set;}
        public DateTime publishDate {get; set;}

        [ForeignKey("post")]
        public int? postId {get; set;}
        public PostModel post {get; set;} = null!;
    }
}