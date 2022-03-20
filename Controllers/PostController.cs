using Microsoft.AspNetCore.Mvc;
using MvcNet.Models;

namespace MvcNet.Controllers {
    public class PostController : Controller {
        private readonly ILogger<PostController> _log;
        private readonly AppDBContext _db;
        public PostController(ILogger<PostController> logger,
                        AppDBContext db) {
            _log = logger;
            _db = db;
        }

        public ActionResult Index() {
            var data = _db.posts.ToList();
            return View("/Views/Blog/Index.cshtml",data);
        }

        [HttpGet("/Post/new")]
        public ActionResult NewPost() {
            return View();
        }

        [HttpPost("/Post/new")]
        public async Task<ActionResult> SaveNewPost(PostModel data) {
            _db.posts.Add(data);
            await _db.SaveChangesAsync();
            TempData["message"] = "Success Add Post";
            return Redirect("~/Post/new");
        }
    }
}