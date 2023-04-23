using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlkho.Data;
using qlkho.Models;

namespace qlkho.Controllers
{
    public class MaterialTopicsController : Controller
    {
        private readonly qlkhoContext _context;

        public MaterialTopicsController(qlkhoContext context)
        {
            _context = context;
        }

        // GET: MaterialTopics
        public async Task<IActionResult> Index()
        {
              return _context.MaterialTopic != null ? 
                          View(await _context.MaterialTopic.ToListAsync()) :
                          Problem("Entity set 'qlkhoContext.MaterialTopic'  is null.");
        }

        // GET: MaterialTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaterialTopic == null)
            {
                return NotFound();
            }

            var materialTopic = await _context.MaterialTopic
                .FirstOrDefaultAsync(m => m.MaterialTopicID == id);
            if (materialTopic == null)
            {
                return NotFound();
            }

            return View(materialTopic);
        }

        // GET: MaterialTopics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaterialTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialTopicID,Name")] MaterialTopic materialTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materialTopic);
        }

        // GET: MaterialTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaterialTopic == null)
            {
                return NotFound();
            }

            var materialTopic = await _context.MaterialTopic.FindAsync(id);
            if (materialTopic == null)
            {
                return NotFound();
            }
            return View(materialTopic);
        }

        // POST: MaterialTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialTopicID,Name")] MaterialTopic materialTopic)
        {
            if (id != materialTopic.MaterialTopicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialTopicExists(materialTopic.MaterialTopicID))
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
            return View(materialTopic);
        }

        // GET: MaterialTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaterialTopic == null)
            {
                return NotFound();
            }

            var materialTopic = await _context.MaterialTopic
                .FirstOrDefaultAsync(m => m.MaterialTopicID == id);
            if (materialTopic == null)
            {
                return NotFound();
            }

            return View(materialTopic);
        }

        // POST: MaterialTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaterialTopic == null)
            {
                return Problem("Entity set 'qlkhoContext.MaterialTopic'  is null.");
            }
            var materialTopic = await _context.MaterialTopic.FindAsync(id);
            if (materialTopic != null)
            {
                _context.MaterialTopic.Remove(materialTopic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialTopicExists(int id)
        {
          return (_context.MaterialTopic?.Any(e => e.MaterialTopicID == id)).GetValueOrDefault();
        }
    }
}
