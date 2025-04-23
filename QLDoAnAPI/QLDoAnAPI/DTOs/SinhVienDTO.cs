using System;
using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class SinhVienDTO
    {
        [Required(ErrorMessage = "Mã sinh viên là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã sinh viên không được vượt quá 10 ký tự.")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string SoDienThoai { get; set; }

        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "Mã lớp là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã lớp không được vượt quá 10 ký tự.")]
        public string MaLop { get; set; }
    }
}