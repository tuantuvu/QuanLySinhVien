using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLySinhVien.Common;
using QuanLySinhVien.Models.ChungLoai;
using QuanLySinhVien.Models.Entity;
using System.Net.WebSockets;
using NuGet.Common;
using NuGet.Protocol;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuanLySinhVien.Controllers
{
    [Route("/quan-ly-khoa")]
    public class KhoaController : Controller
    {
        private static string wwwroot = Directory.GetCurrentDirectory() + "\\wwwroot";
        private readonly QuanLySinhVienContext _context;
        public KhoaController(QuanLySinhVienContext context)
        {
            _context = context;
        }

		//[Route("danh-sach")]
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
        public async Task<IActionResult> Index()
        {
            //call du lieu tu api
            var items = await getKhoa();
            return View(items);
        }

        public async Task<List<ViewModelChungLoai>> getKhoa()
		{
			string url = "http://localhost:5032/api/Khoa/danh-sach-khoa";
			using (var client = new HttpClient())
			{

				var res = await client.GetAsync(url);
				//var resDele = await client.DeleteAsync(url);

				if (res.IsSuccessStatusCode)
				{
					var viewkhoa = new List<ViewModelChungLoai>();
					var listitems = res.Content.ReadAsAsync<List<ResultApiChungLoai>>().Result;
					foreach (var item in listitems)
					{
						ViewModelChungLoai khoa = new ViewModelChungLoai();
						khoa.Id = item.Id;
						khoa.MaKhoa = item.MaKhoa;
						khoa.TenKhoa = item.TenKhoa;
						khoa.SDT = item.SDT;
						//khoa.UrlImages = JsonConvert.DeserializeObject<List<ResultApiImageChungLoai>>(item.UrlImages);
						viewkhoa.Add(khoa);
					}
					return viewkhoa;
				}
			}
			return null;
		}

        [Route("them-khoa", Name = "them")]
        public IActionResult Them()
        {
            return View();
        }
        [Route("them-khoa", Name = "them")]
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Them(Khoa input)
        public async Task<IActionResult> Them(InputChungLoai input)
        {
            string url = "http://localhost:5032/api/Khoa/them-moi-khoa";
            var client = new HttpClient();
            var result = await client.PostAsJsonAsync(url, input);
            return View(result);
        }
            //using (var client = new HttpClient())
            //{
            //    var data = new MultipartFormDataContent();
            //    data.Add(new StringContent(input.MaKhoa), "MaKhoa");
            //    data.Add(new StringContent(input.TenKhoa), "TenKhoa");
            //    data.Add(new StringContent(input.Sdt), "SDT");

            //    var listimg = new List<string>();
            //    foreach (var img in input.hinhanh)
            //    {
            //        var imgPath = UploadFiles.SaveImage(img);
            //        listimg.Add(imgPath);
            //        var imgStream = new MemoryStream(System.IO.File.ReadAllBytes(wwwroot + imgPath));
            //        var imgContent = new ByteArrayContent(imgStream.ToArray());
            //        data.Add(imgContent, "Images", img.FileName);
            //    }

            //    var res = await client.PostAsync(url, data);
            //    //var resPut = await client.PutAsync(url, data);
            //    if (res.IsSuccessStatusCode)
            //    {
            //        foreach (var path in listimg)
            //        {
            //            UploadFiles.RemoveImage(path);
            //        }

            //        return View("Index");
            //    }
            //    else
            //    {
            //        //co the them cac doan thong bao loi~
            //        return View();
            //    }

            //}
            //if (ModelState.IsValid)
            //{
            //    Khoa khoa = new Khoa();
            //    khoa.Id = Guid.NewGuid().ToString();
            //    khoa.MaKhoa = input.MaKhoa;
            //    khoa.TenKhoa = input.TenKhoa;
            //    khoa.Sdt = input.Sdt;
            //    khoa.Filter = input.MaKhoa + " " + input.TenKhoa.ToLower() + " " + Utility.ConvertToUnsign(input.TenKhoa.ToLower()) + " " + input.Sdt;
            //    khoa.UrlImage = UploadFiles.SaveImage(input.hinhanh);

            //    _context.Add(khoa);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View();
        //}
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
