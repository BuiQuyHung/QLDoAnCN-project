using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("Lop")]
    public class Lop
    {
        [Key]
        [Column("MaLop")]
        [MaxLength(10)]
        public string MaLop { get; set; }

        [Required]
        [Column("TenLop")]
        [MaxLength(100)]
        public string TenLop { get; set; }

        [Column("KhoaHoc")]
        [MaxLength(10)]
        public string KhoaHoc { get; set; }

        // Foreign Key đến bảng ChuyenNganh
        [Column("MaChuyenNganh")]
        [MaxLength(10)]
        [ForeignKey("ChuyenNganh")]
        public string MaChuyenNganh { get; set; }

        // Navigation property đến bảng ChuyenNganh
        public ChuyenNganh ChuyenNganh { get; set; }

        // Navigation property đến bảng SinhVien (nếu có)
        // public ICollection<SinhVien> SinhViens { get; set; }
    }
}