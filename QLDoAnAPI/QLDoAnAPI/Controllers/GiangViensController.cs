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
    public class GiangViensController : ControllerBase
    {
        private readonly IGiangVienRepository _giangVienRepository;
        // Nếu bạn thêm MaBoMon, bạn có thể inject IBoMonRepository để kiểm tra

        public GiangViensController(IGiangVienRepository giangVienRepository)
        {
            _giangVienRepository = giangVienRepository;
        }

        // GET: api/GiangViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetGiangViens()
        {
            var giangViens = await _giangVienRepository.GetAllAsync();
            return Ok(giangViens);
        }

        // GET: api/GiangViens/GV001
        [HttpGet("{id}")]
        public async Task<ActionResult<GiangVien>> GetGiangVien(string id)
        {
            var giangVien = await _giangVienRepository.GetByIdAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }
            return Ok(giangVien);
        }

        // POST: api/GiangViens
        [HttpPost]
        public async Task<ActionResult<GiangVienDTO>> PostGiangVien(GiangVienDTO giangVienDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var giangVien = new GiangVien
            {
                MaGV = giangVienDTO.MaGV,
                HoTen = giangVienDTO.HoTen,
                ChuyenNganh = giangVienDTO.ChuyenNganh,
                Email = giangVienDTO.Email,
                SoDienThoai = giangVienDTO.SoDienThoai
                // Nếu bạn thêm MaBoMon, hãy gán giá trị ở đây
            };

            if (await _giangVienRepository.AddAsync(giangVien))
            {
                return CreatedAtAction(nameof(GetGiangVien), new { id = giangVien.MaGV }, giangVienDTO);
            }
            return BadRequest("Không thể thêm giảng viên.");
        }

        // PUT: api/GiangViens/GV001
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiangVien(string id, GiangVienDTO giangVienDTO)
        {
            if (id != giangVienDTO.MaGV)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _giangVienRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            var giangVien = new GiangVien
            {
                MaGV = giangVienDTO.MaGV,
                HoTen = giangVienDTO.HoTen,
                ChuyenNganh = giangVienDTO.ChuyenNganh,
                Email = giangVienDTO.Email,
                SoDienThoai = giangVienDTO.SoDienThoai
                // Nếu bạn thêm MaBoMon, hãy gán giá trị ở đây
            };

            if (await _giangVienRepository.UpdateAsync(giangVien))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật giảng viên.");
        }

        // DELETE: api/GiangViens/GV001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiangVien(string id)
        {
            if (!await _giangVienRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _giangVienRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa giảng viên.");
        }
    }
}