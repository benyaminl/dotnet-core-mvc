using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcNet.Controllers {
    public class UserController : Controller {
        [HttpGet("/login")]
        public IActionResult Login() {
            return View();
        }

    }
}