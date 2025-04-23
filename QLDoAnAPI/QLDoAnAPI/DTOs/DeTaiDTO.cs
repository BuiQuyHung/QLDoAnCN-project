using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class DeTaiDTO
    {
        [Required(ErrorMessage = "Mã đề tài là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã đề tài không được vượt quá 10 ký tự.")]
        public string MaDeTai { get; set; }

        [Required(ErrorMessage = "Tên đề tài là bắt buộc.")]
        [MaxLength(200, ErrorMessage = "Tên đề tài không được vượt quá 200 ký tự.")]
        public string TenDeTai { get; set; }

        public string MoTa { get; set; }

        [MaxLength(100, ErrorMessage = "Chuyên ngành không được vượt quá 100 ký tự.")]
        public string ChuyenNganh { get; set; }

        public int? ThoiGianThucHien { get; set; }

        [Required(ErrorMessage = "Mã giảng viên hướng dẫn là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã giảng viên không được vượt quá 10 ký tự.")]
        public string MaGV { get; set; }

        [MaxLength(50, ErrorMessage = "Trạng thái không được vượt quá 50 ký tự.")]
        public string TrangThai { get; set; }
    }
}