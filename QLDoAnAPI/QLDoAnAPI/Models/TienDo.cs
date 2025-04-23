using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("TienDo")]
    public class TienDo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MaTienDo")]
        public int MaTienDo { get; set; }

        [Required]
        [Column("MaDeTai")]
        [MaxLength(10)]
        [ForeignKey("DeTai")]
        public string MaDeTai { get; set; }

        [Required]
        [Column("MaSV")]
        [MaxLength(10)]
        [ForeignKey("SinhVien")]
        public string MaSV { get; set; }

        [Column("NgayNop")]
        public DateTime? NgayNop { get; set; } = DateTime.Now;

        [Required]
        [Column("LoaiBaoCao")]
        [MaxLength(50)]
        public string LoaiBaoCao { get; set; }

        [Column("TepDinhKem")]
        [MaxLength(255)]
        public string TepDinhKem { get; set; }

        [Column("GhiChu")]
        [MaxLength(500)]
        public string GhiChu { get; set; }
        public DeTai DeTai { get; set; }
        public SinhVien SinhVien { get; set; }
    }
}