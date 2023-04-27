using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlkho.Data;
using qlkho.Libs;
using qlkho.Models;
using System.Diagnostics;

namespace qlkho.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly qlkhoContext _context;
        private readonly INotyfService _notifyService;

        public const string SessionUser = "_UserID";
        public const string SessionUsername = "_Username";
        public const string SessionPassword = "_Password";
        public const string SessionRole = "_Role";

        public HomeController(ILogger<HomeController> logger, qlkhoContext context, INotyfService notyfService)
        {
            _logger = logger;
            _context = context;
            _notifyService = notyfService;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLogin TaiKhoan)
        {
            if (ModelState.IsValid)
            {
                string mahoamatkhau = SHA1.ComputeHash(TaiKhoan.Password);
                var taiKhoan = await _context.User.FirstOrDefaultAsync(r => r.Username == TaiKhoan.Username
                                                                            && r.Password == mahoamatkhau);
                if (taiKhoan == null)
                {
                    _notifyService.Error("Đăng nhập không thành công!");
                }
                else
                {
                    // Đăng ký SESSION
                    HttpContext.Session.SetInt32(SessionUser, (int)taiKhoan.UserID);
                    HttpContext.Session.SetString(SessionUsername, taiKhoan.Username);
                    HttpContext.Session.SetString(SessionPassword, taiKhoan.Password);
                    HttpContext.Session.SetInt32(SessionRole, (int)taiKhoan.RoleID);

                    _notifyService.Success("Đăng nhập thành công!");
                    return RedirectToAction("Index", "Home");
                }
            }
            _notifyService.Error("Đăng nhập không thành công!");
            return View(TaiKhoan);
        }


        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("_UserID") == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("_UserID") != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("_UserID");
            HttpContext.Session.Remove("_Username");
            HttpContext.Session.Remove("_UserID");
            HttpContext.Session.Remove("_UserID");
            return RedirectToAction("Index", "Home");
        }
    }
}