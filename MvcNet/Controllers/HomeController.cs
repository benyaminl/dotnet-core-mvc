using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcNet.Models;
using MvcNet.Models.Request;
using Post.Domain;
using Post.Domain.Models;
using Post.Infrastructure;

namespace MvcNet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPostRepository _postRepository;
    private readonly IPostCommentRepository _commentRepository;

    public HomeController(ILogger<HomeController> logger,
                            IPostCommentRepository commentRepository,
                            IPostRepository postRepository)
    {
        _logger = logger;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
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
            var data = _postRepository.GetPostsAsync().Result;
            return View("/Views/Blog/BlogPostList.cshtml",data);
        } else {
            var data = _postRepository.GetAsync(id).Result;
            
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

        var result = _commentRepository.AddComment(c);
        _commentRepository.SaveChange();

        TempData["message"] = "Success Post new Comments";

        return RedirectToAction("Blog", new { id = id });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
