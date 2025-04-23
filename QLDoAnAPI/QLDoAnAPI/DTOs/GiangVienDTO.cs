using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class GiangVienDTO
    {
        [Required(ErrorMessage = "Mã giảng viên là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã giảng viên không được vượt quá 10 ký tự.")]
        public string MaGV { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        public string HoTen { get; set; }

        [MaxLength(100, ErrorMessage = "Chuyên ngành không được vượt quá 100 ký tự.")]
        public string ChuyenNganh { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string SoDienThoai { get; set; }

        // Nếu bạn muốn thêm MaBoMon vào DTO (tùy chọn)
        // public string MaBoMon { get; set; }
    }
}