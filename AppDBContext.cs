using Microsoft.EntityFrameworkCore;
using MvcNet.Models;

namespace MvcNet {
    public class AppDBContext : DbContext {
        
        public DbSet<UserModel> user {get; set;} = null!;
    }
}