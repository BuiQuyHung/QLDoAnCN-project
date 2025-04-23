using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("GiangVien")]
    public class GiangVien
    {
        [Key]
        [Column("MaGV")]
        [MaxLength(10)]
        public string MaGV { get; set; }

        [Required]
        [Column("HoTen")]
        [MaxLength(100)]
        public string HoTen { get; set; }

        [Column("ChuyenNganh")]
        [MaxLength(100)]
        public string ChuyenNganh { get; set; }

        [Required]
        [Column("Email")]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Column("SoDienThoai")]
        [MaxLength(20)]
        public string SoDienThoai { get; set; }

        // Navigation property đến bảng BoMon (nếu có mối quan hệ)
        // [ForeignKey("BoMon")]
        // public string MaBoMon { get; set; }
        // public BoMon BoMon { get; set; }
    }
}