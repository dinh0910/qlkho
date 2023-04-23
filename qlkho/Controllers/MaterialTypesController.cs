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
    public class MaterialTypesController : Controller
    {
        private readonly qlkhoContext _context;

        public MaterialTypesController(qlkhoContext context)
        {
            _context = context;
        }

        // GET: MaterialTypes
        public async Task<IActionResult> Index()
        {
              return _context.MaterialType != null ? 
                          View(await _context.MaterialType.ToListAsync()) :
                          Problem("Entity set 'qlkhoContext.MaterialType'  is null.");
        }

        // GET: MaterialTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaterialType == null)
            {
                return NotFound();
            }

            var materialType = await _context.MaterialType
                .FirstOrDefaultAsync(m => m.MaterialTypeID == id);
            if (materialType == null)
            {
                return NotFound();
            }

            return View(materialType);
        }

        // GET: MaterialTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaterialTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialTypeID,Name")] MaterialType materialType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materialType);
        }

        // GET: MaterialTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaterialType == null)
            {
                return NotFound();
            }

            var materialType = await _context.MaterialType.FindAsync(id);
            if (materialType == null)
            {
                return NotFound();
            }
            return View(materialType);
        }

        // POST: MaterialTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialTypeID,Name")] MaterialType materialType)
        {
            if (id != materialType.MaterialTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialTypeExists(materialType.MaterialTypeID))
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
            return View(materialType);
        }

        // GET: MaterialTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaterialType == null)
            {
                return NotFound();
            }

            var materialType = await _context.MaterialType
                .FirstOrDefaultAsync(m => m.MaterialTypeID == id);
            if (materialType == null)
            {
                return NotFound();
            }

            return View(materialType);
        }

        // POST: MaterialTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaterialType == null)
            {
                return Problem("Entity set 'qlkhoContext.MaterialType'  is null.");
            }
            var materialType = await _context.MaterialType.FindAsync(id);
            if (materialType != null)
            {
                _context.MaterialType.Remove(materialType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialTypeExists(int id)
        {
          return (_context.MaterialType?.Any(e => e.MaterialTypeID == id)).GetValueOrDefault();
        }
    }
}
