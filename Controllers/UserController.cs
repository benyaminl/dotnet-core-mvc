using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcNet.Controllers {
    public class UserController : Controller {
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
    }
}