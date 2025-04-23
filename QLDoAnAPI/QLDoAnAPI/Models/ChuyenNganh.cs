using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("ChuyenNganh")]
    public class ChuyenNganh
    {
        [Key]
        [Column("MaChuyenNganh")]
        [MaxLength(10)]
        public string MaChuyenNganh { get; set; }

        [Required]
        [Column("TenChuyenNganh")]
        [MaxLength(100)]
        public string TenChuyenNganh { get; set; }

        // Foreign Key đến bảng Nganh (một ChuyenNganh thuộc về một Nganh)
        [Column("MaNganh")]
        [MaxLength(10)]
        [ForeignKey("Nganh")]
        public string MaNganh { get; set; }

        // Navigation property đến bảng Nganh
        public Nganh Nganh { get; set; }

        // Navigation property đến bảng SinhVien (nếu có mối quan hệ)
        // public ICollection<SinhVien> SinhViens { get; set; }
    }
}