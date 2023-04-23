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
    public class MaterialNamesController : Controller
    {
        private readonly qlkhoContext _context;

        public MaterialNamesController(qlkhoContext context)
        {
            _context = context;
        }

        // GET: MaterialNames
        public async Task<IActionResult> Index()
        {
            var qlkhoContext = _context.MaterialName.Include(m => m.MaterialTopic).Include(m => m.MaterialType);
            return View(await qlkhoContext.ToListAsync());
        }

        // GET: MaterialNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var materialName = _context.MaterialLog
                .Include(m => m.Material).Include(m => m.Material.MaterialName).Include(m => m.User)
                .Where(m => m.Material.MaterialNameID == id);

            if (materialName == null)
            {
                return NotFound();
            }

            return View(materialName);
        }

        // GET: MaterialNames/Create
        public IActionResult Create()
        {
            ViewData["MaterialTopicID"] = new SelectList(_context.MaterialTopic, "MaterialTopicID", "Name");
            ViewData["MaterialTypeID"] = new SelectList(_context.MaterialType, "MaterialTypeID", "Name");
            return View();
        }

        // POST: MaterialNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialNameID,Name,MaterialTypeID,MaterialTopicID,Count")] MaterialName materialName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialTopicID"] = new SelectList(_context.MaterialTopic, "MaterialTopicID", "Name", materialName.MaterialTopicID);
            ViewData["MaterialTypeID"] = new SelectList(_context.MaterialType, "MaterialTypeID", "Name", materialName.MaterialTypeID);
            return View(materialName);
        }

        // GET: MaterialNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaterialName == null)
            {
                return NotFound();
            }

            var materialName = await _context.MaterialName.FindAsync(id);
            if (materialName == null)
            {
                return NotFound();
            }
            ViewData["MaterialTopicID"] = new SelectList(_context.MaterialTopic, "MaterialTopicID", "Name", materialName.MaterialTopicID);
            ViewData["MaterialTypeID"] = new SelectList(_context.MaterialType, "MaterialTypeID", "Name", materialName.MaterialTypeID);
            return View(materialName);
        }

        // POST: MaterialNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialNameID,Name,MaterialTypeID,MaterialTopicID,Count")] MaterialName materialName)
        {
            if (id != materialName.MaterialNameID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialNameExists(materialName.MaterialNameID))
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
            ViewData["MaterialTopicID"] = new SelectList(_context.MaterialTopic, "MaterialTopicID", "Name", materialName.MaterialTopicID);
            ViewData["MaterialTypeID"] = new SelectList(_context.MaterialType, "MaterialTypeID", "Name", materialName.MaterialTypeID);
            return View(materialName);
        }

        // GET: MaterialNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaterialName == null)
            {
                return NotFound();
            }

            var materialName = await _context.MaterialName
                .Include(m => m.MaterialTopic)
                .Include(m => m.MaterialType)
                .FirstOrDefaultAsync(m => m.MaterialNameID == id);
            if (materialName == null)
            {
                return NotFound();
            }

            return View(materialName);
        }

        // POST: MaterialNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaterialName == null)
            {
                return Problem("Entity set 'qlkhoContext.MaterialName'  is null.");
            }
            var materialName = await _context.MaterialName.FindAsync(id);
            if (materialName != null)
            {
                _context.MaterialName.Remove(materialName);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialNameExists(int id)
        {
          return (_context.MaterialName?.Any(e => e.MaterialNameID == id)).GetValueOrDefault();
        }
    }
}
