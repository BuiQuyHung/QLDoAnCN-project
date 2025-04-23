// Controllers/DeTaisController.cs
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
    public class DeTaisController : ControllerBase
    {
        private readonly IDeTaiRepository _deTaiRepository;
        private readonly IGiangVienRepository _giangVienRepository; // Để kiểm tra sự tồn tại của GiangVien

        public DeTaisController(IDeTaiRepository deTaiRepository, IGiangVienRepository giangVienRepository)
        {
            _deTaiRepository = deTaiRepository;
            _giangVienRepository = giangVienRepository;
        }

        // GET: api/DeTais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeTai>>> GetDeTais()
        {
            var deTais = await _deTaiRepository.GetAllAsync();
            return Ok(deTais);
        }

        // GET: api/DeTais/DT001
        [HttpGet("{id}")]
        public async Task<ActionResult<DeTai>> GetDeTai(string id)
        {
            var deTai = await _deTaiRepository.GetByIdAsync(id);
            if (deTai == null)
            {
                return NotFound();
            }
            return Ok(deTai);
        }

        // POST: api/DeTais
        [HttpPost]
        public async Task<ActionResult<DeTaiDTO>> PostDeTai(DeTaiDTO deTaiDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _giangVienRepository.ExistsAsync(deTaiDTO.MaGV))
            {
                return BadRequest($"Không tồn tại giảng viên có mã: {deTaiDTO.MaGV}");
            }

            var deTai = new DeTai
            {
                MaDeTai = deTaiDTO.MaDeTai,
                TenDeTai = deTaiDTO.TenDeTai,
                MoTa = deTaiDTO.MoTa,
                ChuyenNganh = deTaiDTO.ChuyenNganh,
                ThoiGianThucHien = deTaiDTO.ThoiGianThucHien,
                MaGV = deTaiDTO.MaGV,
                TrangThai = deTaiDTO.TrangThai
            };

            if (await _deTaiRepository.AddAsync(deTai))
            {
                return CreatedAtAction(nameof(GetDeTai), new { id = deTai.MaDeTai }, deTaiDTO);
            }
            return BadRequest("Không thể thêm đề tài.");
        }

        // PUT: api/DeTais/DT001
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeTai(string id, DeTaiDTO deTaiDTO)
        {
            if (id != deTaiDTO.MaDeTai)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _deTaiRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _giangVienRepository.ExistsAsync(deTaiDTO.MaGV))
            {
                return BadRequest($"Không tồn tại giảng viên có mã: {deTaiDTO.MaGV}");
            }

            var deTai = new DeTai
            {
                MaDeTai = deTaiDTO.MaDeTai,
                TenDeTai = deTaiDTO.TenDeTai,
                MoTa = deTaiDTO.MoTa,
                ChuyenNganh = deTaiDTO.ChuyenNganh,
                ThoiGianThucHien = deTaiDTO.ThoiGianThucHien,
                MaGV = deTaiDTO.MaGV,
                TrangThai = deTaiDTO.TrangThai
            };

            if (await _deTaiRepository.UpdateAsync(deTai))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật đề tài.");
        }

        // DELETE: api/DeTais/DT001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeTai(string id)
        {
            if (!await _deTaiRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _deTaiRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa đề tài.");
        }
    }
}