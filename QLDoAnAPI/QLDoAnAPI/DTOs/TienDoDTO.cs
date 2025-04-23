using System;
using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class TienDoDTO
    {
        [Required(ErrorMessage = "Mã đề tài là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã đề tài không được vượt quá 10 ký tự.")]
        public string MaDeTai { get; set; }

        [Required(ErrorMessage = "Mã sinh viên là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã sinh viên không được vượt quá 10 ký tự.")]
        public string MaSV { get; set; }

        public DateTime? NgayNop { get; set; }

        [Required(ErrorMessage = "Loại báo cáo là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Loại báo cáo không được vượt quá 50 ký tự.")]
        public string LoaiBaoCao { get; set; }

        [MaxLength(255, ErrorMessage = "Tệp đính kèm không được vượt quá 255 ký tự.")]
        public string TepDinhKem { get; set; }

        [MaxLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự.")]
        public string GhiChu { get; set; }
    }
}