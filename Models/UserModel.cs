using System.ComponentModel.DataAnnotations;

namespace MvcNet.Models
{
    public class UserModel {
        [Key]
        public int id {get; set;}
        public string user {get; set;} = null!;
        public string pass {get; set;} = null!;
        public string role {get; set;} = null!;
    }
}