using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QLDoAnAPI.Models
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [Column("MaSV")]
        [MaxLength(10)]
        public string MaSV { get; set; }

        [Required]
        [Column("HoTen")]
        [MaxLength(100)]
        public string HoTen { get; set; }

        [Required]
        [Column("Email")]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Column("SoDienThoai")]
        [MaxLength(20)]
        public string SoDienThoai { get; set; }

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        // Foreign Key đến bảng Lop
        [Column("MaLop")]
        [MaxLength(10)]
        [ForeignKey("Lop")]
        public string MaLop { get; set; }

        // Navigation property đến bảng Lop
        public Lop Lop { get; set; }
    }
}