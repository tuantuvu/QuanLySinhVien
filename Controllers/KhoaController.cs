using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Common;
using QuanLySinhVien.Models.ChungLoai;
using QuanLySinhVien.Models.Entity;
using System.Net.WebSockets;

namespace QuanLySinhVien.Controllers
{
    [Route("/quan-ly-khoa")]
    public class KhoaController : Controller
    {
        private readonly QuanLySinhVienContext _context;
        public KhoaController(QuanLySinhVienContext context)
        {
            _context = context;
        }
        [Route("danh-sach")]
        public IActionResult Index(string? timkiem)
        {
            //var items = _context.Khoas.Where(c => c.Filter.Contains((timkiem ?? "").ToLower())).ToList();
            //return View(items);

            if (String.IsNullOrWhiteSpace(timkiem))
            {
                var items1 = _context.Khoas.ToList();
                return View(items1);
            }
            else
            {
                var items2 = _context.Khoas.Where(c => c.Filter.Contains(timkiem)).ToList();
                return View(items2);
            }
        }
        [Route("them-khoa")]
        public IActionResult Them()
        {
            return View();
        }
        [Route("them-khoa")]
        [HttpPost]
        public IActionResult Them( InputChungLoai input)
        {
            if (ModelState.IsValid)
            {
                Khoa khoa = new Khoa();
                khoa.Id = Guid.NewGuid().ToString();
                khoa.MaKhoa = input.MaKhoa;
                khoa.TenKhoa = input.TenKhoa;
                khoa.Sdt = input.Sdt;
                khoa.Filter = input.MaKhoa + " " + input.TenKhoa.ToLower() + " " + Utility.ConvertToUnsign(input.TenKhoa.ToLower()) + " " + input.Sdt;
                khoa.UrlImage = UploadFiles.SaveImage(input.hinhanh);

                _context.Add(khoa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        return View();

    }
        //[Route("them-khoa")]
        //[HttpPost]
        //public IActionResult Them(string makhoa, string tenkhoa, string sodienthoai, IFormFile hinhanh)
        //{
        //    //tạo Guid
        //    //Guid id = Guid.NewGuid();
        //    if (!string.IsNullOrEmpty(makhoa))
        //    {
        //        Khoa khoa = new Khoa();
        //        khoa.Id = Guid.NewGuid().ToString();
        //        khoa.MaKhoa = makhoa;
        //        khoa.TenKhoa = tenkhoa;
        //        khoa.Sdt = sodienthoai;
        //        khoa.Filter = makhoa + " " + tenkhoa.ToLower() + " " + Utility.ConvertToUnsign(tenkhoa.ToLower()) + " " + sodienthoai;
        //        khoa.UrlImage = UploadFiles.SaveImage(hinhanh);

        //        _context.Add(khoa);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //        //return Redirect("/Khoa/Index");
        //    }
        //    return View();
        //}

        [Route("cap-nhat")]
        public IActionResult CapNhat(string id)
        {
            var item = _context.Khoas.FirstOrDefault(x => x.Id == id);
            return View(item);
        }

        [Route("cap-nhat")]
        [HttpPost]
        public IActionResult CapNhat(string id, string? makhoa, string tenkhoa, string sodienthoai)
        {
            var item = _context.Khoas.FirstOrDefault(x => x.Id == id);
            item.MaKhoa = makhoa;
            item.TenKhoa = tenkhoa;
            item.Sdt = sodienthoai;
            _context.Update(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("xoa-khoa")]
        public IActionResult Xoa(string id)
        {
            var item = _context.Khoas.FirstOrDefault(x => x.Id == id);
            bool check = UploadFiles.RemoveImage(item.UrlImage);
           {
                _context.Remove(item);
                _context.SaveChanges();
           }
            return RedirectToAction("Index");
        }
    }
}
