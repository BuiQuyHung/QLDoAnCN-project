using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("HoiDong")]
    public class HoiDong
    {
        [Key]
        [Column("MaHoiDong")]
        [MaxLength(10)]
        public string MaHoiDong { get; set; }

        [Required]
        [Column("TenHoiDong")]
        [MaxLength(100)]
        public string TenHoiDong { get; set; }

        [Column("NgayBaoVe")]
        public DateTime? NgayBaoVe { get; set; }

        //public icollection<thanhvienhoidong> thanhvienhoidongs { get; set; }
    }
}