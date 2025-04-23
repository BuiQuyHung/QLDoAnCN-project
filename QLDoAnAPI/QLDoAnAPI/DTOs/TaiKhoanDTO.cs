using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class TaiKhoanDTO
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự.")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Mật khẩu không được vượt quá 100 ký tự.")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc.")]
        [MaxLength(20, ErrorMessage = "Vai trò không được vượt quá 20 ký tự.")]
        public string VaiTro { get; set; }

        [MaxLength(10, ErrorMessage = "Mã người dùng không được vượt quá 10 ký tự.")]
        public string MaNguoiDung { get; set; }
    }

    public class TaiKhoanUpdateDTO
    {
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Mật khẩu không được vượt quá 100 ký tự.")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc.")]
        [MaxLength(20, ErrorMessage = "Vai trò không được vượt quá 20 ký tự.")]
        public string VaiTro { get; set; }

        [MaxLength(10, ErrorMessage = "Mã người dùng không được vượt quá 10 ký tự.")]
        public string MaNguoiDung { get; set; }
    }
}