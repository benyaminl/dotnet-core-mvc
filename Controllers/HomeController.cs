using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcNet.Models;

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
            
            var data = _db.posts.Find(id);
            return View("/Views/Blog/BlogPostDetail.cshtml",data);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
