using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("DeTai")]
    public class DeTai
    {
        [Key]
        [Column("MaDeTai")]
        [MaxLength(10)]
        public string MaDeTai { get; set; }

        [Required]
        [Column("TenDeTai")]
        [MaxLength(200)]
        public string TenDeTai { get; set; }

        [Column("MoTa")]
        public string MoTa { get; set; }

        [Column("ChuyenNganh")]
        [MaxLength(100)]
        public string ChuyenNganh { get; set; }

        [Column("ThoiGianThucHien")]
        public int? ThoiGianThucHien { get; set; }

        // Foreign Key đến bảng GiangVien (người hướng dẫn)
        [Column("MaGV")]
        [MaxLength(10)]
        [ForeignKey("GiangVien")]
        public string MaGV { get; set; }

        // Navigation property đến bảng GiangVien
        public GiangVien GiangVien { get; set; }

        [Column("TrangThai")]
        [MaxLength(50)]
        public string TrangThai { get; set; } = "Chờ duyệt";
    }
}