using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [Column("TenDangNhap")]
        [MaxLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [Column("MatKhau")]
        [MaxLength(100)]
        public string MatKhau { get; set; }

        [Required]
        [Column("VaiTro")]
        [MaxLength(20)]
        public string VaiTro { get; set; }

        [Column("MaNguoiDung")]
        [MaxLength(10)]
        public string MaNguoiDung { get; set; }
    }
}