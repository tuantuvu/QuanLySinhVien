using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.Common;
using QuanLySinhVien.Models.Entity;

namespace QuanLySinhVien.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("/quan-tri-vien")]

    public class HomeController : Controller
    {
        //private readonly QuanLySinhVienContext _context;
        //public HomeController(QuanLySinhVienContext context)
        //{
        //    _context = context;
        //}
        ////[Route("danh-sach")]
        //public IActionResult Index(string? timkiem)
        //{
        //    var items = _context.Khoas.Where(c => c.Filter.Contains((timkiem ?? "").ToLower())).ToList();
        //    return View(items);

        //    if (String.IsNullOrWhiteSpace(timkiem))
        //    {
        //        var items1 = _context.Khoas.ToList();
        //        return View(items1);
        //    }
        //    else
        //    {
        //        var items2 = _context.Khoas.Where(c => c.Filter.Contains(timkiem)).ToList();
        //        return View(items2);
        //    }
        //}
        [Route("danh-sach")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("them")]
        public IActionResult Them()
        {
            return View();
        }
        [Route("cap-nhat")]
        public IActionResult CapNhat()
        {
            return View();
        }
    }
}
