namespace QuanLySinhVien.Models.ChungLoai
{
	public class ViewModelChungLoai
	{
		public string Id { get; set; }
		public string MaKhoa { get; set; }
		public string TenKhoa { get; set; }
		public string SDT { get; set; }
		public List<ResultApiImageChungLoai> UrlImages { get; set; }
	}
}
