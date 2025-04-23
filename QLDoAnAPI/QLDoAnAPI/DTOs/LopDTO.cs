using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class LopDTO
    {
        [Required(ErrorMessage = "Mã lớp là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã lớp không được vượt quá 10 ký tự.")]
        public string MaLop { get; set; }

        [Required(ErrorMessage = "Tên lớp là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Tên lớp không được vượt quá 100 ký tự.")]
        public string TenLop { get; set; }

        [MaxLength(10, ErrorMessage = "Khóa học không được vượt quá 10 ký tự.")]
        public string KhoaHoc { get; set; }

        [Required(ErrorMessage = "Mã chuyên ngành là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã chuyên ngành không được vượt quá 10 ký tự.")]
        public string MaChuyenNganh { get; set; }
    }
}