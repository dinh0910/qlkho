using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using qlkho.Data;
using qlkho.Models;

namespace qlkho.Controllers
{
    public class ImportsController : Controller
    {
        private readonly qlkhoContext _context;

        public ImportsController(qlkhoContext context)
        {
            _context = context;
        }

        // GET: Imports
        public async Task<IActionResult> Index()
        {
            var qlkhoContext = _context.Import.Include(i => i.Supplier).Include(i => i.User);
            ViewData["MaterialNameID"] = new SelectList(_context.MaterialName, "MaterialNameID", "Name");
            ViewData["UnitID"] = new SelectList(_context.Unit, "UnitID", "Name");
            return View(await qlkhoContext.ToListAsync());
        }

        // GET: Imports/Create
        public IActionResult Create()
        {
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID");
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID");
            return View();
        }

        // POST: Imports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportID,SupplierID,UserID,DateCreated")] Import import)
        {
            if (ModelState.IsValid)
            {
                _context.Add(import);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", import.SupplierID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", import.UserID);
            return View(import);
        }

        // GET: Imports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Import == null)
            {
                return NotFound();
            }

            var import = await _context.Import.FindAsync(id);
            if (import == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", import.SupplierID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", import.UserID);
            return View(import);
        }

        // POST: Imports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImportID,SupplierID,UserID,DateCreated")] Import import)
        {
            if (id != import.ImportID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(import);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportExists(import.ImportID))
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
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", import.SupplierID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", import.UserID);
            return View(import);
        }

        // GET: Imports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Import == null)
            {
                return NotFound();
            }

            var import = await _context.Import
                .Include(i => i.Supplier)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.ImportID == id);
            if (import == null)
            {
                return NotFound();
            }

            return View(import);
        }

        // POST: Imports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Import == null)
            {
                return Problem("Entity set 'qlkhoContext.Import'  is null.");
            }
            var import = await _context.Import.FindAsync(id);
            if (import != null)
            {
                _context.Import.Remove(import);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportExists(int id)
        {
          return (_context.Import?.Any(e => e.ImportID == id)).GetValueOrDefault();
        }

        public const string CARTIMPORT = "addcart";

        List<ImportItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTIMPORT);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<ImportItem>>(jsoncart);
            }
            return new List<ImportItem>();
        }

        void SaveCartSession(List<ImportItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("addcart", jsoncart);
        }

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("addcart");
        }

        public async Task<IActionResult> AddToCart([Bind("MaterialNameID,Expiry,Quantity,UnitID")] ImportItem importItem)
        {
            var product = await _context.MaterialName
                .FirstOrDefaultAsync(m => m.MaterialNameID == importItem.MaterialNameID);
            var dvt = await _context.Unit
                .FirstOrDefaultAsync(m => m.UnitID == importItem.UnitID);
            //if (product == null)
            //{
            //    _toastNotification.AddInfoToastMessage("Sản phẩm không tồn tại.");
            //}
            var cart = GetCartItems();
            var item = cart.Find(p => p.MaterialName.MaterialNameID == importItem.MaterialNameID);
            if (item != null)
            {
                item.Quantity += importItem.Quantity;
            }
            else
            {
                cart.Add(new ImportItem() { MaterialName = product, Expiry = importItem.Expiry, Unit = dvt, Quantity = importItem.Quantity });
            }
            SaveCartSession(cart);
            //return RedirectToAction(nameof(ViewImport));
            return RedirectToAction("Index", "Imports");
        }

        [Route("/updateitem", Name = "updateitem")]
        public async Task<IActionResult> UpdateItem(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.MaterialName.MaterialNameID == id);
            if (quantity == 0)
            {
                cart.Remove(item);
            }
            item.Quantity = quantity;
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewImport));
        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.MaterialName.MaterialNameID == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewImport));
        }

        [Route("/viewimport", Name = "import")]
        public IActionResult ViewImport()
        {
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "Name");
            return View(GetCartItems());
        }

        public async Task<IActionResult> CreateBill(int SupplierID)
        {
            // lưu hóa đơn
            var bill = new Import();
            bill.DateCreated = DateTime.Now;
            bill.SupplierID = SupplierID;
            bill.UserID = (int)HttpContext.Session.GetInt32("_UserID");

            _context.Add(bill);
            await _context.SaveChangesAsync();

            var cart = GetCartItems();

            //chi tiết hóa đơn
            foreach (var i in cart)
            {
                var b = new ImportLog();
                b.ImportID = bill.ImportID;
                b.MaterialNameID = i.MaterialName.MaterialNameID;
                b.UnitID = i.Unit.UnitID;
                b.Quantity = i.Quantity;
                for(int j = 0; j < i.Quantity; j++)
                {
                    var d = new Material();
                    d.MaterialNameID = b.MaterialNameID;
                    d.Expiry = i.Expiry;
                    d.Status = 0;
                    _context.Add(d);
                    await _context.SaveChangesAsync();
                }

                var sp = _context.MaterialName.FirstOrDefault(s => s.MaterialNameID == b.MaterialNameID);
                sp.Count += i.Quantity;
                _context.Add(b);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
            ClearCart();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Import == null)
            {
                return NotFound();
            }

            ViewBag.importlog = _context.ImportLog.Include(s => s.MaterialName).Include(s => s.Unit);

            var import = _context.Import.Include(i => i.User).Include(i => i.Supplier)
                .FirstOrDefault(m => m.ImportID == id);
            if (import == null)
            {
                return NotFound();
            }

            return View(import);
        }
    }
}
