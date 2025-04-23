using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class KhoaDTO
    {
        [Required(ErrorMessage = "Mã khoa là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã khoa không được vượt quá 10 ký tự.")]
        public string MaKhoa { get; set; }

        [Required(ErrorMessage = "Tên khoa là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Tên khoa không được vượt quá 100 ký tự.")]
        public string TenKhoa { get; set; }
    }
}