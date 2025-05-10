using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

public class TaiKhoanDangNhap
{
    [Key]
    public string TenDangNhap { get; set; }

    [Required]
    public string MatKhau { get; set; }

    [Required]
    public string VaiTro { get; set; }

    [Required]
    public string MaNguoiDung { get; set; }
}
