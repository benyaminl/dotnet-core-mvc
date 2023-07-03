using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Domain.Models
{
    public class PostTagsModel {
        [Key]
        [Column(Order=1)]
        public int postId {get; set;}
        [Key]
        [Column(Order=2)]
        public int tagId {get; set;}
    }
}