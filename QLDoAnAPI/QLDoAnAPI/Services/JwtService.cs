//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using QLDoAnAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace QLDoAnAPI.Services
//{
//    public class JwtService
//    {
//        private readonly JwtSettings _jwtSettings;

//        public JwtService(IOptions<JwtSettings> jwtSettings)
//        {
//            _jwtSettings = jwtSettings.Value;
//        }

//        public string GenerateToken(TaiKhoan taiKhoan)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, taiKhoan.TenDangNhap),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(ClaimTypes.Role, taiKhoan.VaiTro),
//            };

//            // Thêm MaGV hoặc MaSV vào claim, tùy thuộc vào VaiTro và giá trị có tồn tại
//            if (taiKhoan.VaiTro == "GV" && !string.IsNullOrEmpty(taiKhoan.MaGV))
//            {
//                claims.Add(new Claim("MaGV", taiKhoan.MaGV));
//            }
//            else if (taiKhoan.VaiTro == "SV" && !string.IsNullOrEmpty(taiKhoan.MaSV))
//            {
//                claims.Add(new Claim("MaSV", taiKhoan.MaSV));
//            }

//            var token = new JwtSecurityToken(
//                issuer: _jwtSettings.Issuer,
//                audience: _jwtSettings.Audience,
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }

//    public class JwtSettings
//    {
//        public string Secret { get; set; }
//        public string Issuer { get; set; }
//        public string Audience { get; set; }
//        public int ExpirationMinutes { get; set; }
//    }
//}
