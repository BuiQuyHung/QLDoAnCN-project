using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class DanhGiaDTO
    {
        [Required(ErrorMessage = "Mã đề tài là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã đề tài không được vượt quá 10 ký tự.")]
        public string MaDeTai { get; set; }

        [Required(ErrorMessage = "Mã giảng viên là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã giảng viên không được vượt quá 10 ký tự.")]
        public string MaGV { get; set; }

        [Required(ErrorMessage = "Điểm số là bắt buộc.")]
        [Range(0, 10, ErrorMessage = "Điểm số phải nằm trong khoảng từ 0 đến 10.")]
        public float DiemSo { get; set; }

        public string NhanXet { get; set; }
    }
}