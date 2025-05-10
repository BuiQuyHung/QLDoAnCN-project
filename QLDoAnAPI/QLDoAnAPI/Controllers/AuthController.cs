using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System;
using QLDoAnAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly QLDoAnChuyenNganhDbContext _context;

    public AuthController(QLDoAnChuyenNganhDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.TaiKhoanDangNhap
            .FirstOrDefaultAsync(u => u.TenDangNhap == request.TenDangNhap);

        if (user == null || user.MatKhau != request.MatKhau)
            return Unauthorized("Sai tài khoản hoặc mật khẩu");

        return Ok(new { Message = "Đăng nhập thành công", VaiTro = user.VaiTro });
    }

    //private bool VerifyPassword(string inputPassword, string storedHash)
    //{
    //    using (SHA256 sha256 = SHA256.Create())
    //    {
    //        var hashedInput = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword)));
    //        return hashedInput == storedHash;
    //    }
    //}
}