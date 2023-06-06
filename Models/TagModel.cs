using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNet.Models
{
    public class TagModel {
        [Key]
        public int id {get; set;}
        [Required]
        public string tagName {get; set;} = null!;
        
        [ForeignKey("tagId")]
        public ICollection<PostTagsModel> blogTags {get; set;} = null!;
    }
}
