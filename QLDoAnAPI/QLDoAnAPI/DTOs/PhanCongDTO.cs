using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class PhanCongDTO
    {
        [Required(ErrorMessage = "Mã đề tài là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã đề tài không được vượt quá 10 ký tự.")]
        public string MaDeTai { get; set; }

        [Required(ErrorMessage = "Mã sinh viên là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã sinh viên không được vượt quá 10 ký tự.")]
        public string MaSV { get; set; }
    }
}