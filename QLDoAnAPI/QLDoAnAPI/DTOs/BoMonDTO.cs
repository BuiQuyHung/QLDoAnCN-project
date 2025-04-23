// DTOs/BoMonDTO.cs
using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class BoMonDTO
    {
        [Required(ErrorMessage = "Mã bộ môn là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã bộ môn không được vượt quá 10 ký tự.")]
        public string MaBoMon { get; set; }

        [Required(ErrorMessage = "Tên bộ môn là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Tên bộ môn không được vượt quá 100 ký tự.")]
        public string TenBoMon { get; set; }

        [Required(ErrorMessage = "Mã khoa là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã khoa không được vượt quá 10 ký tự.")]
        public string MaKhoa { get; set; }
    }
}