// Models/Khoa.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QLDoAnAPI.Models
{
    [Table("Khoa")]
    public class Khoa
    {
        [Key]
        [Column("MaKhoa")]
        [MaxLength(10)]
        public string MaKhoa { get; set; }

        [Required]
        [Column("TenKhoa")]
        [MaxLength(100)]
        public string TenKhoa { get; set; }

        // Navigation property đến bảng BoMon (một Khoa có thể có nhiều BoMon)
        public ICollection<BoMon> BoMons { get; set; }
    }
}