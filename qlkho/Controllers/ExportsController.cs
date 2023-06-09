﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using qlkho.Data;
using qlkho.Models;
using XAct;

namespace qlkho.Controllers
{
    public class ExportsController : Controller
    {
        private readonly qlkhoContext _context;

        public ExportsController(qlkhoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var qlkhoContext = _context.Export.Include(i => i.User);
            ViewData["MaterialNameID"] = new SelectList(_context.MaterialName, "MaterialNameID", "Name");
            ViewData["UnitID"] = new SelectList(_context.Unit, "UnitID", "Name");
            ViewBag.material = _context.MaterialName;
            ViewBag.m = _context.Material.Include(s => s.MaterialName).Where(s => s.Status == 0);
            return View(await qlkhoContext.ToListAsync());
        }

        public const string CARTEXPORT = "addexport";

        List<ExportItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTEXPORT);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<ExportItem>>(jsoncart);
            }
            return new List<ExportItem>();
        }

        void SaveCartSession(List<ExportItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("addexport", jsoncart);
        }

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("addexport");
        }

        public async Task<IActionResult> AddToCart([Bind("MaterialNameID,Quantity,UnitID")] ExportItem exportItem)
        {
            var product = await _context.MaterialName
                .FirstOrDefaultAsync(m => m.MaterialNameID == exportItem.MaterialNameID);
            var dvt = await _context.Unit
                .FirstOrDefaultAsync(m => m.UnitID == exportItem.UnitID);
            //if (product == null)
            //{
            //    _toastNotification.AddInfoToastMessage("Sản phẩm không tồn tại.");
            //}
            var cart = GetCartItems();
            var item = cart.Find(p => p.MaterialName.MaterialNameID == exportItem.MaterialNameID);
            if (item != null)
            {
                item.Quantity += exportItem.Quantity;
            }
            else
            {
                cart.Add(new ExportItem() { MaterialName = product, Unit = dvt, Quantity = exportItem.Quantity });
            }
            SaveCartSession(cart);
            //return RedirectToAction(nameof(ViewImport));
            return RedirectToAction("Index", "Exports");
        }

        [Route("/updateexport", Name = "updateexport")]
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
            return RedirectToAction(nameof(ViewExport));
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
            return RedirectToAction(nameof(ViewExport));
        }

        [Route("/viewexport", Name = "export")]
        public IActionResult ViewExport()
        {
            return View(GetCartItems());
        }

        public async Task<IActionResult> CreateBill()
        {
            // lưu hóa đơn
            var bill = new Export();
            bill.DateCreated = DateTime.Now;
            bill.UserID = (int)HttpContext.Session.GetInt32("_UserID");

            _context.Add(bill);
            await _context.SaveChangesAsync();

            var cart = GetCartItems();
            //chi tiết hóa đơn
            foreach (var i in cart)
            {
                var b = new ExportLog();
                b.ExportID = bill.ExportID;
                b.MaterialNameID = i.MaterialName.MaterialNameID;
                b.UnitID = i.Unit.UnitID;
                b.Quantity = i.Quantity;

                var count = i.Quantity;
                var d = _context.Material.Where(s => s.MaterialNameID == i.MaterialName.MaterialNameID && s.Status == 0);
                foreach (var item in d)
                {
                    if (0 < count)
                    {
                        item.Status = 2;
                        count--;
                    }
                }

                _context.Add(b);
            }
            await _context.SaveChangesAsync();
            ClearCart();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Export == null)
            {
                return NotFound();
            }

            ViewBag.exportlog = _context.ExportLog.Include(s => s.MaterialName).Include(s => s.Unit);

            var import = _context.Export.Include(i => i.User)
                .FirstOrDefault(m => m.ExportID == id);
            if (import == null)
            {
                return NotFound();
            }

            return View(import);
        }
    }
}
