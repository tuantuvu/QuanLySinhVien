using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Common;
using QuanLySinhVien.Models.Entity;
using System.Net.WebSockets;

namespace QuanLySinhVien.Views.Khoa
{
    public class UserKhoaController : Controller
	{
        
        private readonly QuanLySinhVienContext _context;
        public UserKhoaController(QuanLySinhVienContext context)
        {
            _context = context;
        }
        public IActionResult UserKhoa()
		{
            var items = _context.Khoas.ToList();
            return View(items);
		}
	}
}
