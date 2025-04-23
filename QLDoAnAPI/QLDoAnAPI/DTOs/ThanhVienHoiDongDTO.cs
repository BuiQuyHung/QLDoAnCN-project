using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class ThanhVienHoiDongDTO
    {
        [Required(ErrorMessage = "Mã hội đồng là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã hội đồng không được vượt quá 10 ký tự.")]
        public string MaHoiDong { get; set; }

        [Required(ErrorMessage = "Mã giảng viên là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã giảng viên không được vượt quá 10 ký tự.")]
        public string MaGV { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Vai trò không được vượt quá 50 ký tự.")]
        public string VaiTro { get; set; }
    }
}