using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("ThanhVienHoiDong")]
    public class ThanhVienHoiDong
    {
        [Key]
        [Column("MaHoiDong", Order = 0)]
        [MaxLength(10)]
        [ForeignKey("HoiDong")]
        public string MaHoiDong { get; set; }

        [Key]
        [Column("MaGV", Order = 1)]
        [MaxLength(10)]
        [ForeignKey("GiangVien")]
        public string MaGV { get; set; }

        [Required]
        [Column("VaiTro")]
        [MaxLength(50)]
        public string VaiTro { get; set; }

        public HoiDong HoiDong { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}