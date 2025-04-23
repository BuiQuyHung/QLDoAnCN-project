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
    public class HoiDongsController : ControllerBase
    {
        private readonly IHoiDongRepository _hoiDongRepository;

        public HoiDongsController(IHoiDongRepository hoiDongRepository)
        {
            _hoiDongRepository = hoiDongRepository;
        }

        // GET: api/HoiDongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoiDong>>> GetHoiDongs()
        {
            var hoiDongs = await _hoiDongRepository.GetAllAsync();
            return Ok(hoiDongs);
        }

        // GET: api/HoiDongs/HD01
        [HttpGet("{id}")]
        public async Task<ActionResult<HoiDong>> GetHoiDong(string id)
        {
            var hoiDong = await _hoiDongRepository.GetByIdAsync(id);
            if (hoiDong == null)
            {
                return NotFound();
            }
            return Ok(hoiDong);
        }

        // POST: api/HoiDongs
        [HttpPost]
        public async Task<ActionResult<HoiDongDTO>> PostHoiDong(HoiDongDTO hoiDongDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _hoiDongRepository.ExistsAsync(hoiDongDTO.MaHoiDong))
            {
                return Conflict($"Mã hội đồng '{hoiDongDTO.MaHoiDong}' đã tồn tại.");
            }

            var hoiDong = new HoiDong
            {
                MaHoiDong = hoiDongDTO.MaHoiDong,
                TenHoiDong = hoiDongDTO.TenHoiDong,
                NgayBaoVe = hoiDongDTO.NgayBaoVe
            };

            if (await _hoiDongRepository.AddAsync(hoiDong))
            {
                return CreatedAtAction(nameof(GetHoiDong), new { id = hoiDong.MaHoiDong }, hoiDong);
            }
            return BadRequest("Không thể thêm hội đồng.");
        }

        // PUT: api/HoiDongs/HD01
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoiDong(string id, HoiDongDTO hoiDongDTO)
        {
            if (id != hoiDongDTO.MaHoiDong)
            {
                return BadRequest("Mã hội đồng trong URL không khớp với mã trong dữ liệu.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _hoiDongRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            var existingHoiDong = await _hoiDongRepository.GetByIdAsync(id);
            if (existingHoiDong == null)
            {
                return NotFound();
            }

            existingHoiDong.TenHoiDong = hoiDongDTO.TenHoiDong;
            existingHoiDong.NgayBaoVe = hoiDongDTO.NgayBaoVe;

            if (await _hoiDongRepository.UpdateAsync(existingHoiDong))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật hội đồng.");
        }

        // DELETE: api/HoiDongs/HD01
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoiDong(string id)
        {
            if (!await _hoiDongRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _hoiDongRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa hội đồng.");
        }
    }
}