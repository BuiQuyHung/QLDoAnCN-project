// Controllers/KhoasController.cs
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
    public class KhoasController : ControllerBase
    {
        private readonly IKhoaRepository _khoaRepository;

        public KhoasController(IKhoaRepository khoaRepository)
        {
            _khoaRepository = khoaRepository;
        }

        // GET: api/Khoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhoaDTO>>> GetKhoas()
        {
            var khoas = await _khoaRepository.GetAllAsync();
            return Ok(khoas); // Trả về trực tiếp model hoặc map sang DTO nếu cần
        }

        // GET: api/Khoas/CNTT
        [HttpGet("{id}")]
        public async Task<ActionResult<KhoaDTO>> GetKhoa(string id)
        {
            var khoa = await _khoaRepository.GetByIdAsync(id);
            if (khoa == null)
            {
                return NotFound();
            }
            return Ok(khoa); // Trả về trực tiếp model hoặc map sang DTO nếu cần
        }

        // POST: api/Khoas
        [HttpPost]
        public async Task<ActionResult<KhoaDTO>> PostKhoa(KhoaDTO khoaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var khoa = new Khoa { MaKhoa = khoaDTO.MaKhoa, TenKhoa = khoaDTO.TenKhoa };
            if (await _khoaRepository.AddAsync(khoa))
            {
                return CreatedAtAction(nameof(GetKhoa), new { id = khoa.MaKhoa }, khoaDTO);
            }
            return BadRequest("Không thể thêm khoa.");
        }

        // PUT: api/Khoas/CNTT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhoa(string id, KhoaDTO khoaDTO)
        {
            if (id != khoaDTO.MaKhoa)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _khoaRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            var khoa = new Khoa { MaKhoa = khoaDTO.MaKhoa, TenKhoa = khoaDTO.TenKhoa };
            if (await _khoaRepository.UpdateAsync(khoa))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật khoa.");
        }

        // DELETE: api/Khoas/CNTT
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhoa(string id)
        {
            if (!await _khoaRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _khoaRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa khoa.");
        }
    }
}