using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class DotDoAnRequestDTO
    {
        [Required(ErrorMessage = "Mã đợt đồ án là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã đợt đồ án không được vượt quá 10 ký tự.")]
        public string MaDotDoAn { get; set; }

        [Required(ErrorMessage = "Tên đợt đồ án là bắt buộc.")]
        [MaxLength(200, ErrorMessage = "Tên đợt đồ án không được vượt quá 200 ký tự.")]
        public string TenDotDoAn { get; set; }

        [Required(ErrorMessage = "Khóa học là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Khóa học không được vượt quá 10 ký tự.")]
        public string KhoaHoc { get; set; }

        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? SoTuanThucHien { get; set; }
    }

    public class DotDoAnDTO
    {
        public string MaDotDoAn { get; set; }
        public string TenDotDoAn { get; set; }
        public string KhoaHoc { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? SoTuanThucHien { get; set; }
    }
}