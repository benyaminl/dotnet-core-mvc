using Microsoft.AspNetCore.Mvc;
using MvcNet.Areas.Identity.Data;
using System.Text.Encodings.Web;

namespace MvcNet.Controllers {
    public class UserController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;

        private readonly MvcNetIdentityDbContext _idb;
        public UserController(ILogger<HomeController> logger,
                            AppDBContext db,
                            MvcNetIdentityDbContext idx)
        {
            _logger = logger;
            _db = db;
            _idb = idx;
        }

        [HttpGet("/login")]
        public IActionResult Login() {
            return View();
        }
        
        /// @ see https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-6.0#require-antiforgery-validation
        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(string user, string pass) {
            ViewData["user"] = user;
            ViewData["pass"] = pass;
            return View(new {data = user});
        }

        public IActionResult ListUser()
        {
            var users = _idb.Users.ToList();

            return View("/Views/User/ListUser.cshtml", users);
        }
    }
}