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

        [HttpGet("/Post")]
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
            _log.LogInformation("coba");
            return Redirect("~/Post/new");
        }

        [HttpGet("/Post/{id}/delete")]
        public async Task<ActionResult> DeletePost(int id) {
            // @see  https://stackoverflow.com/a/56537176/4906348
            var post = new PostModel {
                id = id
            };
            _db.posts.Remove(post);
            await _db.SaveChangesAsync();
            TempData["message"] = "Success Delete Post";
            return Redirect("~/Post");
        }

        [HttpGet("/Post/{id}")]
        public ActionResult UpdatePost(int id) {
            var data = _db.posts.Where(i => i.id == id).Single();

            return View("/Views/Post/EditPost.cshtml",data);
        }

        [HttpGet("/Post/{id}")]
        public async Task<ActionResult> UpdatePostData(int id, PostModel data) {
            _db.posts.Update(data);
            await _db.SaveChangesAsync();
            
            return View("/Views/Post/EditPost.cshtml",data);
        }
    }
}
