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
    public class SinhViensController : ControllerBase
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly ILopRepository _lopRepository; // Để kiểm tra sự tồn tại của Lop

        public SinhViensController(ISinhVienRepository sinhVienRepository, ILopRepository lopRepository)
        {
            _sinhVienRepository = sinhVienRepository;
            _lopRepository = lopRepository;
        }

        // GET: api/SinhViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhViens()
        {
            var sinhViens = await _sinhVienRepository.GetAllAsync();
            return Ok(sinhViens);
        }

        // GET: api/SinhViens/SV001
        [HttpGet("{id}")]
        public async Task<ActionResult<SinhVien>> GetSinhVien(string id)
        {
            var sinhVien = await _sinhVienRepository.GetByIdAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return Ok(sinhVien);
        }

        // POST: api/SinhViens
        [HttpPost]
        public async Task<ActionResult<SinhVienDTO>> PostSinhVien(SinhVienDTO sinhVienDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _lopRepository.ExistsAsync(sinhVienDTO.MaLop))
            {
                return BadRequest($"Không tồn tại lớp có mã: {sinhVienDTO.MaLop}");
            }

            var sinhVien = new SinhVien
            {
                MaSV = sinhVienDTO.MaSV,
                HoTen = sinhVienDTO.HoTen,
                Email = sinhVienDTO.Email,
                SoDienThoai = sinhVienDTO.SoDienThoai,
                NgaySinh = sinhVienDTO.NgaySinh,
                MaLop = sinhVienDTO.MaLop
            };

            if (await _sinhVienRepository.AddAsync(sinhVien))
            {
                return CreatedAtAction(nameof(GetSinhVien), new { id = sinhVien.MaSV }, sinhVienDTO);
            }
            return BadRequest("Không thể thêm sinh viên.");
        }

        // PUT: api/SinhViens/SV001
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSinhVien(string id, SinhVienDTO sinhVienDTO)
        {
            if (id != sinhVienDTO.MaSV)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _sinhVienRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _lopRepository.ExistsAsync(sinhVienDTO.MaLop))
            {
                return BadRequest($"Không tồn tại lớp có mã: {sinhVienDTO.MaLop}");
            }

            var sinhVien = new SinhVien
            {
                MaSV = sinhVienDTO.MaSV,
                HoTen = sinhVienDTO.HoTen,
                Email = sinhVienDTO.Email,
                SoDienThoai = sinhVienDTO.SoDienThoai,
                NgaySinh = sinhVienDTO.NgaySinh,
                MaLop = sinhVienDTO.MaLop
            };

            if (await _sinhVienRepository.UpdateAsync(sinhVien))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật sinh viên.");
        }

        // DELETE: api/SinhViens/SV001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSinhVien(string id)
        {
            if (!await _sinhVienRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _sinhVienRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa sinh viên.");
        }
    }
}