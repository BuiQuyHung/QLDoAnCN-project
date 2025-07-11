﻿using System;
using System.ComponentModel.DataAnnotations;

namespace QLDoAnAPI.DTOs
{
    public class HoiDongDTO
    {
        [Required(ErrorMessage = "Mã hội đồng là bắt buộc.")]
        [MaxLength(10, ErrorMessage = "Mã hội đồng không được vượt quá 10 ký tự.")]
        public string MaHoiDong { get; set; }

        [Required(ErrorMessage = "Tên hội đồng là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Tên hội đồng không được vượt quá 100 ký tự.")]
        public string TenHoiDong { get; set; }

        public DateTime? NgayBaoVe { get; set; }
    }
}