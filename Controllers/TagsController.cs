#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcNet;
using MvcNet.Models;

namespace MvcNet.Controllers
{
    public class TagsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly ILogger<TagsController> _log;

        public TagsController(AppDBContext context, ILogger<TagsController> log)
        {
            _context = context;
            _log = log;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await _context.tags.ToListAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagModel = await _context.tags
                .FirstOrDefaultAsync(m => m.id == id);
            if (tagModel == null)
            {
                return NotFound();
            }

            return View(tagModel);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,tagName")] TagModel tagModel)
        {
            _log.LogError("Error");
            if (ModelState.IsValid)
            {
              _context.Add(tagModel);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
            } else {
                _log.LogInformation("err0r");
              foreach (var modelState in ViewData.ModelState.Values)
              {
                  TempData["error"] = modelState.Errors; 
                  foreach (ModelError error in modelState.Errors)
                  {
                      _log.LogInformation(error.ErrorMessage);
                  }
              }
            }
            return View(tagModel);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagModel = await _context.tags.FindAsync(id);
            if (tagModel == null)
            {
                return NotFound();
            }
            return View(tagModel);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,tagName")] TagModel tagModel)
        {
            if (id != tagModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagModelExists(tagModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tagModel);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagModel = await _context.tags
                .FirstOrDefaultAsync(m => m.id == id);
            if (tagModel == null)
            {
                return NotFound();
            }

            return View(tagModel);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagModel = await _context.tags.FindAsync(id);
            _context.tags.Remove(tagModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagModelExists(int id)
        {
            return _context.tags.Any(e => e.id == id);
        }
    }
}
