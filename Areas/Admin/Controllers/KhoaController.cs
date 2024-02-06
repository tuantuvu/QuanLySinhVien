using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using QuanLySinhVien.Models.ChungLoai;

namespace QuanLySinhVien.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("khoa")]
    public class KhoaController : Controller
    {
        private readonly HttpClient _httpClient;
        public KhoaController(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Client");
        }
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

            var res = await _httpClient.GetAsync(url);

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
                    khoa.UrlImages = JsonConvert.DeserializeObject<List<ResultApiImageChungLoai>>(item.UrlImages);
                    viewkhoa.Add(khoa);
                }
                return viewkhoa;
            }
            return null;
        }
    }
}
