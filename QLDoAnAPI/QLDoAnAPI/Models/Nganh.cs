using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QLDoAnAPI.Models
{
    [Table("Nganh")]
    public class Nganh
    {
        [Key]
        [Column("MaNganh")]
        [MaxLength(10)]
        public string MaNganh { get; set; }

        [Required]
        [Column("TenNganh")]
        [MaxLength(100)]
        public string TenNganh { get; set; }

        // Foreign Key đến bảng BoMon (một Nganh thuộc về một BoMon)
        [Column("MaBoMon")]
        [MaxLength(10)]
        [ForeignKey("BoMon")]
        public string MaBoMon { get; set; }

        // Navigation property đến các bảng 
        public BoMon BoMon { get; set; }
        public ICollection<ChuyenNganh> ChuyenNganhs { get; set; }

        // Navigation property đến bảng SinhVien (một Nganh có thể có nhiều SinhVien)
        //public ICollection<SinhVien> SinhViens { get; set; }
    }
}