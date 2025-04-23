using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("BoMon")]
    public class BoMon
    {
        [Key]
        [Column("MaBoMon")]
        [MaxLength(10)]
        public string MaBoMon { get; set; }

        [Required]
        [Column("TenBoMon")]
        [MaxLength(100)]
        public string TenBoMon { get; set; }

        // Foreign Key đến bảng Khoa
        [Column("MaKhoa")]
        [MaxLength(10)]
        [ForeignKey("Khoa")]
        public string MaKhoa { get; set; }

        // Navigation property đến bảng Khoa
        public Khoa Khoa { get; set; }

        // Navigation property đến bảng Nganh (nếu có)
        public ICollection<Nganh> Nganhs { get; set; }
    }
}