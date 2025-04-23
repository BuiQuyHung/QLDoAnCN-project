// DTOs/ChuyenNganhDTO.cs
using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class ChuyenNganhDTO
    {
        [Required(ErrorMessage = "Mã chuyên ngành là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã chuyên ngành không được vượt quá 10 ký tự.")]
        public string MaChuyenNganh { get; set; }

        [Required(ErrorMessage = "Tên chuyên ngành là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Tên chuyên ngành không được vượt quá 100 ký tự.")]
        public string TenChuyenNganh { get; set; }

        [Required(ErrorMessage = "Mã ngành là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã ngành không được vượt quá 10 ký tự.")]
        public string MaNganh { get; set; }
    }
}