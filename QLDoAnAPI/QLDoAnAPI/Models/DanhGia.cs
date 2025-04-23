using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAnAPI.Models
{
    [Table("DanhGia")]
    public class DanhGia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MaDanhGia")]
        public int MaDanhGia { get; set; }

        [Required]
        [Column("MaDeTai")]
        [MaxLength(10)]
        [ForeignKey("DeTai")]
        public string MaDeTai { get; set; }

        [Required]
        [Column("MaGV")]
        [MaxLength(10)]
        [ForeignKey("GiangVien")]
        public string MaGV { get; set; }

        [Required]
        [Column("DiemSo")]
        public double DiemSo { get; set; }

        [Column("NhanXet")]
        public string NhanXet { get; set; }

        public DeTai DeTai { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}