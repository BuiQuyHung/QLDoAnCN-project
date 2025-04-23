// Models/PhanCong.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("PhanCong")]
    public class PhanCong
    {
        [Key]
        [Column("MaDeTai", Order = 0)]
        [MaxLength(10)]
        [ForeignKey("DeTai")]
        public string MaDeTai { get; set; }

        [Key]
        [Column("MaSV", Order = 1)]
        [MaxLength(10)]
        [ForeignKey("SinhVien")]
        public string MaSV { get; set; }

        [Column("NgayPhanCong")]
        public DateTime? NgayPhanCong { get; set; }
        public DeTai DeTai { get; set; }
        public SinhVien SinhVien { get; set; }
    }
}