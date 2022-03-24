using Microsoft.AspNetCore.Mvc;

namespace MvcNet.Controllers {
  public class TagController : Controller {
    private readonly AppDBContext _db;
    private readonly ILogger<TagController> _log;

    public TagController(ILogger<TagController> log, AppDBContext db) {
      _log = log;
      _db = db; 
    }

    public IActionResult Coba() {
      return Ok();
    }
  }
}
