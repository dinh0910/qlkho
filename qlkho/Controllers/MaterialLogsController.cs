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
    public class MaterialLogsController : Controller
    {
        private readonly qlkhoContext _context;

        public MaterialLogsController(qlkhoContext context)
        {
            _context = context;
        }

        // GET: MaterialLogs
        public async Task<IActionResult> Index()
        {
            var qlkhoContext = _context.MaterialLog.Include(m => m.Material).Include(m => m.User);
            return View(await qlkhoContext.ToListAsync());
        }

        // GET: MaterialLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaterialLog == null)
            {
                return NotFound();
            }

            var materialLog = await _context.MaterialLog
                .Include(m => m.Material)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MaterialLogID == id);
            if (materialLog == null)
            {
                return NotFound();
            }

            return View(materialLog);
        }

        // GET: MaterialLogs/Create
        public IActionResult Create()
        {
            ViewData["MaterialID"] = new SelectList(_context.Material, "MaterialID", "MaterialID");
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID");
            return View();
        }

        // POST: MaterialLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialLogID,MaterialID,Stored,TakeAway,TookAway,Returned,UserID,Description")] MaterialLog materialLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialID"] = new SelectList(_context.Material, "MaterialID", "MaterialID", materialLog.MaterialID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", materialLog.UserID);
            return View(materialLog);
        }

        // GET: MaterialLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaterialLog == null)
            {
                return NotFound();
            }

            var materialLog = await _context.MaterialLog.FindAsync(id);
            if (materialLog == null)
            {
                return NotFound();
            }
            ViewData["MaterialID"] = new SelectList(_context.Material, "MaterialID", "MaterialID", materialLog.MaterialID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", materialLog.UserID);
            return View(materialLog);
        }

        // POST: MaterialLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialLogID,MaterialID,Stored,TakeAway,TookAway,Returned,UserID,Description")] MaterialLog materialLog)
        {
            if (id != materialLog.MaterialLogID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialLogExists(materialLog.MaterialLogID))
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
            ViewData["MaterialID"] = new SelectList(_context.Material, "MaterialID", "MaterialID", materialLog.MaterialID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", materialLog.UserID);
            return View(materialLog);
        }

        // GET: MaterialLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaterialLog == null)
            {
                return NotFound();
            }

            var materialLog = await _context.MaterialLog
                .Include(m => m.Material)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MaterialLogID == id);
            if (materialLog == null)
            {
                return NotFound();
            }

            return View(materialLog);
        }

        // POST: MaterialLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaterialLog == null)
            {
                return Problem("Entity set 'qlkhoContext.MaterialLog'  is null.");
            }
            var materialLog = await _context.MaterialLog.FindAsync(id);
            if (materialLog != null)
            {
                _context.MaterialLog.Remove(materialLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialLogExists(int id)
        {
          return (_context.MaterialLog?.Any(e => e.MaterialLogID == id)).GetValueOrDefault();
        }
    }
}
