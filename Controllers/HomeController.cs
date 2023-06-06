using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcNet.Models;
using MvcNet.Models.Request;

namespace MvcNet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDBContext _db;

    public HomeController(ILogger<HomeController> logger,
                        AppDBContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Blog(int id)
    {
        if (id == 0) {
            var data = _db.posts.ToList();
            return View("/Views/Blog/BlogPostList.cshtml",data);
        } else {
            
            var data = _db.posts
                .Include(d => d.comments)
                .First(d => d.id == id);
            
            return View("/Views/Blog/BlogPostDetail.cshtml",data);
        }
    }

    public async Task<IActionResult> Comment(int id, [FromForm] CommentRequest request)
    {

        var c = new PostCommentModel();
        c.name = request.name;
        c.email = request.email;
        c.content = request.content;
        c.insertDate = DateTime.Now;
        c.publishDate = DateTime.Now;
        c.postId = id;

        var result = _db.commentModels.Add(c);
        await _db.SaveChangesAsync();

        TempData["message"] = "Success Post new Comments";

        return RedirectToAction("Blog", new { id = id });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
