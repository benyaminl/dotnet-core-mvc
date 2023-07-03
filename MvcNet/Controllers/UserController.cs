using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcNet.Areas.Identity.Data;
using MvcNet.Models.Request;
using System.Text.Encodings.Web;

namespace MvcNet.Controllers {
    public class UserController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;

        private readonly MvcNetIdentityDbContext _idb;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ILogger<HomeController> logger,
                            AppDBContext db,
                            MvcNetIdentityDbContext idx, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _db = db;
            _idb = idx;
            _userManager = userManager;
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

        [HttpGet("/User")]
        public IActionResult ListUser()
        {
            var users = _idb.Users.ToList();

            return View("/Views/User/ListUser.cshtml", users);
        }

        [Authorize]
        [HttpGet("/User/{id}")]
        public IActionResult FormEditUser(string id)
        {
            var user = _idb.Users.Find(id);

            return View("/Views/User/EditUser.cshtml", user);
        }

        [Authorize]
        [HttpPost("/User/{id}/Password")]
        public async Task<IActionResult> EditPassword(string id, [FromForm] EditPasswordRequest body)
        {
            if (body.newPassword != body.confirmNewPassword)
            {
                return RedirectToAction("FormEditUser",new {id = id});
            }

            var user = await _idb.Users.FindAsync(id);
            if (user == null) 
            {
                return NotFound();
            }

            // Update pass when success
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, body.newPassword);
            if (result.Succeeded)
            {
                TempData["message"] = "Success Change Password";
            }
            else 
            {
                TempData["message"] = "Failed Change Password : " + result.Errors.ToString();
            }

            return RedirectToAction("ListUser");
        }
    }
}