using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using qlkho.Data;
using qlkho.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using XAct;

namespace qlkho.Controllers
{
    public class LendsController : Controller
    {
        private readonly qlkhoContext _context;

        public LendsController(qlkhoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var qlkhoContext = _context.Lend.Include(i => i.User);
            ViewData["MaterialNameID"] = new SelectList(_context.MaterialName, "MaterialNameID", "Name");
            ViewData["UnitID"] = new SelectList(_context.Unit, "UnitID", "Name");
            return View(await qlkhoContext.ToListAsync());
        }

        public const string CARTLEND = "addlend";

        List<LendItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTLEND);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<LendItem>>(jsoncart);
            }
            return new List<LendItem>();
        }

        void SaveCartSession(List<LendItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("addlend", jsoncart);
        }

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("addlend");
        }

        public async Task<IActionResult> AddToLend([Bind("MaterialNameID,Quantity,UnitID")] LendItem lendItem)
        {
            var product = await _context.MaterialName
                .FirstOrDefaultAsync(m => m.MaterialNameID == lendItem.MaterialNameID);
            var dvt = await _context.Unit
                .FirstOrDefaultAsync(m => m.UnitID == lendItem.UnitID);
            //if (product == null)
            //{
            //    _toastNotification.AddInfoToastMessage("Sản phẩm không tồn tại.");
            //}
            var cart = GetCartItems();
            var item = cart.Find(p => p.MaterialName.MaterialNameID == lendItem.MaterialNameID);
            if (item != null)
            {
                item.Quantity += lendItem.Quantity;
            }
            else
            {
                cart.Add(new LendItem() { MaterialName = product, Unit = dvt, Quantity = lendItem.Quantity });
            }
            SaveCartSession(cart);
            //return RedirectToAction(nameof(ViewImport));
            return RedirectToAction("Index", "Lends");
        }

        [Route("/updatelend", Name = "updatelend")]
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
            return RedirectToAction(nameof(ViewLend));
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
            return RedirectToAction(nameof(ViewLend));
        }

        [Route("/viewlend", Name = "lend")]
        public IActionResult ViewLend()
        {
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "Name");
            return View(GetCartItems());
        }

        public async Task<IActionResult> CreateBill(string OrganizationName)
        {
            // lưu hóa đơn
            var bill = new Lend();
            bill.DateCreated = DateTime.Now;
            bill.UserID = (int)HttpContext.Session.GetInt32("_UserID");
            bill.OrganizationName = OrganizationName;

            _context.Add(bill);
            await _context.SaveChangesAsync();

            var cart = GetCartItems();

            //chi tiết hóa đơn
            foreach (var i in cart)
            {
                var b = new LendLog();
                b.LendID = bill.LendID;
                b.MaterialNameID = i.MaterialName.MaterialNameID;
                b.UnitID = i.Unit.UnitID;
                var count = i.Quantity;
                var d = _context.Material.Where(s => s.MaterialNameID == i.MaterialName.MaterialNameID && s.Status == 0);
                foreach (var item in d)
                {
                    if(0 < count)
                    {
                        item.Status = 1;
                        count--;
                    }
                }
                _context.Add(b);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
            ClearCart();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lend == null)
            {
                return NotFound();
            }

            ViewBag.importlog = _context.LendLog.Include(s => s.MaterialName).Include(s => s.Unit);

            var import = _context.Lend.Include(i => i.User)
                .FirstOrDefault(m => m.LendID == id);
            if (import == null)
            {
                return NotFound();
            }

            return View(import);
        }

    }
}
