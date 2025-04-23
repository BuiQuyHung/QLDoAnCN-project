// DTOs/NganhDTO.cs
using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class NganhDTO
    {
        [Required(ErrorMessage = "Mã ngành là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã ngành không được vượt quá 10 ký tự.")]
        public string MaNganh { get; set; }

        [Required(ErrorMessage = "Tên ngành là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Tên ngành không được vượt quá 100 ký tự.")]
        public string TenNganh { get; set; }

        [Required(ErrorMessage = "Mã bộ môn là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã bộ môn không được vượt quá 10 ký tự.")]
        public string MaBoMon { get; set; }
    }
}