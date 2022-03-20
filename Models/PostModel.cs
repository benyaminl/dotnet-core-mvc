using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNet.Models
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
    }
}