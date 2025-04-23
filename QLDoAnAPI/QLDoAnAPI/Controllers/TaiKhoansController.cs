using Microsoft.AspNetCore.Mvc;
using QLDoAnAPI.DTOs;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoansController : ControllerBase
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public TaiKhoansController(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
        }

        // GET: api/TaiKhoans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetTaiKhoans()
        {
            var taiKhoans = await _taiKhoanRepository.GetAllAsync();
            return Ok(taiKhoans);
        }

        // GET: api/TaiKhoans/tendangnhap
        [HttpGet("{tenDangNhap}")]
        public async Task<ActionResult<TaiKhoan>> GetTaiKhoan(string tenDangNhap)
        {
            var taiKhoan = await _taiKhoanRepository.GetByIdAsync(tenDangNhap);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return Ok(taiKhoan);
        }

        // GET: api/TaiKhoans/ByMaNguoiDung/masv01
        [HttpGet("ByMaNguoiDung/{maNguoiDung}")]
        public async Task<ActionResult<TaiKhoan>> GetTaiKhoanByMaNguoiDung(string maNguoiDung)
        {
            var taiKhoan = await _taiKhoanRepository.GetByMaNguoiDungAsync(maNguoiDung);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return Ok(taiKhoan);
        }

        // POST: api/TaiKhoans
        [HttpPost]
        public async Task<ActionResult<TaiKhoanDTO>> PostTaiKhoan(TaiKhoanDTO taiKhoanDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _taiKhoanRepository.ExistsAsync(taiKhoanDTO.TenDangNhap))
            {
                return Conflict($"Tên đăng nhập '{taiKhoanDTO.TenDangNhap}' đã tồn tại.");
            }

            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = taiKhoanDTO.TenDangNhap,
                MatKhau = taiKhoanDTO.MatKhau, // Cần mã hóa mật khẩu trước khi lưu
                VaiTro = taiKhoanDTO.VaiTro,
                MaNguoiDung = taiKhoanDTO.MaNguoiDung
            };

            // TODO: Mã hóa mật khẩu trước khi lưu vào database
            // Ví dụ: taiKhoan.MatKhau = HashPassword(taiKhoanDTO.MatKhau);

            if (await _taiKhoanRepository.AddAsync(taiKhoan))
            {
                return CreatedAtAction(nameof(GetTaiKhoan), new { tenDangNhap = taiKhoan.TenDangNhap }, taiKhoan);
            }
            return BadRequest("Không thể thêm tài khoản.");
        }

        // PUT: api/TaiKhoans/tendangnhap
        [HttpPut("{tenDangNhap}")]
        public async Task<IActionResult> PutTaiKhoan(string tenDangNhap, TaiKhoanUpdateDTO taiKhoanUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTaiKhoan = await _taiKhoanRepository.GetByIdAsync(tenDangNhap);
            if (existingTaiKhoan == null)
            {
                return NotFound();
            }

            existingTaiKhoan.MatKhau = taiKhoanUpdateDTO.MatKhau; // Cần mã hóa mật khẩu trước khi lưu
            existingTaiKhoan.VaiTro = taiKhoanUpdateDTO.VaiTro;
            existingTaiKhoan.MaNguoiDung = taiKhoanUpdateDTO.MaNguoiDung;

            // TODO: Mã hóa mật khẩu trước khi cập nhật
            // existingTaiKhoan.MatKhau = HashPassword(taiKhoanUpdateDTO.MatKhau);

            if (await _taiKhoanRepository.UpdateAsync(existingTaiKhoan))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật tài khoản.");
        }

        // DELETE: api/TaiKhoans/tendangnhap
        [HttpDelete("{tenDangNhap}")]
        public async Task<IActionResult> DeleteTaiKhoan(string tenDangNhap)
        {
            if (!await _taiKhoanRepository.ExistsAsync(tenDangNhap))
            {
                return NotFound();
            }

            if (await _taiKhoanRepository.DeleteAsync(tenDangNhap))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa tài khoản.");
        }
    }
}