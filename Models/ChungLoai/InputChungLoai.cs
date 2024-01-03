using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models.ChungLoai
{
    public class InputChungLoai
    {
        [Required(ErrorMessage = "Mã Sản Phẩm Không được phép để trống!")]
        public string MaKhoa { get; set; }
        [Required(ErrorMessage = "Tên Sản Phẩm sKhông được phép để trống!")]

        public string TenKhoa { get; set; }

        public string? Sdt { get; set; }
        public IFormFile? hinhanh { get; set; }
    }
}
