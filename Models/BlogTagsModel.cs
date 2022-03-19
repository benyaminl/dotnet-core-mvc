using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNet.Models
{
    public class BlogTagsModel {
        [Key]
        public int id {get; set;}
        public string title {get; set;} = null!;
        public string content {get; set;} = null!;
        public DateTime insertDate {get; set;}
        public DateTime publishDate {get; set;}
        
        [ForeignKey("id")]
        public ICollection<TagModel> tags {get; set;} = null!;
    }
}